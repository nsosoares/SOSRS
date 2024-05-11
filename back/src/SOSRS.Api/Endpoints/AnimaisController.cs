using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOSRS.Api.Entities;
using SOSRS.Api.Mappings;
using SOSRS.Api.Repositories;
using SOSRS.Api.ViewModels.Animais;

namespace SOSRS.Api.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimaisController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IAbrigoRepository _abrigoRepository;


        public AnimaisController(IAnimalRepository animalRepository, IAbrigoRepository abrigoRepository)
        {
            _animalRepository =
                animalRepository
                ?? throw new ArgumentNullException(nameof(animalRepository));

            _abrigoRepository =
                abrigoRepository
                ?? throw new ArgumentNullException(nameof(abrigoRepository));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAnimaisPorAbrigo([FromQuery(Name = "abrigo")] int abrigoId)
        {
            var animais = await _animalRepository.GetAnimalPorAbrigo(abrigoId);

            if (animais.Count <= 0)
            {
                return NotFound();
            }

            var response = new
            {
                abrigoId,
                animais
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> CriarAnimal([FromBody] CreateAnimalRequest animal)
        {
            var abrigo = await _abrigoRepository.GetAbrigoPorIdAsync(animal.AbrigoId);

            if(abrigo is null)
            {
                return BadRequest("Abrigo inválido");
            }

            if(!abrigo.PermiteAnimais)
            {
                return BadRequest("Abrigo não permite animais");
            }

            var animalEntity = animal.MapToAnimal();

            var createdEntity = await _animalRepository.CreateAsync(animalEntity);

            if (createdEntity.Id > 0)
            {
                return Created();
            }

            return StatusCode(500, new
            {
                message = "Falha ao cadastrar animal"
            });
        }

        [Authorize]
        [HttpPut()]
        public async Task<IActionResult> AtualizaAnimal([FromBody] EditAnimalRequest animais)
        {
            var mappedAnimal = animais.MapToAnimal();

            var editedEntity = await _animalRepository.EditAsync(mappedAnimal);

            if (editedEntity.Id >= 0)
            {
                return Ok(editedEntity);
            }

            return StatusCode(500, new
            {
                message = "Falha ao atualizar animal"
            });
        }

        [Authorize]
        [HttpDelete()]
        public async Task<IActionResult> DeletarAnimal([FromRoute(Name = "id")] int animalId)
        {
            var deletedAnimal = await _animalRepository.DeleteAsync(animalId);

            if (deletedAnimal)
            {
                return Ok(deletedAnimal);
            }

            return StatusCode(500, new
            {
                message = "Falha ao deletar animal"
            });
        }
    }
}
