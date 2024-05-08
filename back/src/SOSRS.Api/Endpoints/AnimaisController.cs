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

        public AnimaisController(IAnimalRepository animalRepository)
        {
            _animalRepository =
                animalRepository
                ?? throw new ArgumentNullException(nameof(animalRepository));
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
            var animalEntity = animal.MapToAnimal();

            var createdEntity = await _animalRepository.CreateAsync(animalEntity);

            if (createdEntity.Id >= 0)
            {
                return Created();
            }

            return StatusCode(500, new
            {
                message = "Falha ao registrar animal"
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
        public async Task<IActionResult> DeletarAnimal([FromBody] int animalId)
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
