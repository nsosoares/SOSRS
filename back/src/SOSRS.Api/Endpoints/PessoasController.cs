using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOSRS.Api.Entities;
using SOSRS.Api.Extensions;
using SOSRS.Api.Helpers;
using SOSRS.Api.Mappings;
using SOSRS.Api.Repositories;
using SOSRS.Api.ViewModels;

namespace SOSRS.Api.Endpoints
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoasController : Controller
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoasController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository =
                pessoaRepository
                ?? throw new ArgumentNullException(nameof(pessoaRepository));
        }

        // /pessoas?q={criterioDeBuscaAqui}
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "s")] string criterioDeBusca)
        {
            if (!criterioDeBusca.ContainsValue())
            {
                return BadRequest();
            }

            var resultados = await _pessoaRepository.Buscar(criterioDeBusca);

            if (resultados.Count <= 0)
            {
                return NotFound();
            }

            var abrigosMencionados = new List<Abrigo>();

            foreach (var resultado in resultados)
            {
                if (!abrigosMencionados.Contains(resultado.Abrigo))
                {
                    abrigosMencionados.Add(resultado.Abrigo);
                }
            }

            var response = new
            {
                abrigos = abrigosMencionados,
                pessoas = resultados
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PessoaDesaparecidaViewModel pessoaDesaparecida)
        {
            var abrigos = HttpContext.GetAbrigos();

            if(abrigos.Count <= 0)
            {
                return Unauthorized();
            }

            if (!abrigos.Any(s => s == pessoaDesaparecida.AbrigoGuid))
            {
                return Forbid();
            }

            var mappedPessoa = pessoaDesaparecida.MapToPessoaDesaparecidaModel();
            var pessoaCadastrada = await _pessoaRepository.Criar(mappedPessoa);

            if (!pessoaCadastrada)
            {
                return StatusCode(500, new { message = "Falha ao cadastrar pessoa" });
            }

            return Created();
        }
    }
}
