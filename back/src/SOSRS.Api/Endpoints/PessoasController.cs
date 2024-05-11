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
        private readonly IAbrigoRepository _abrigoRepository;

        public PessoasController(IPessoaRepository pessoaRepository, IAbrigoRepository abrigoRepository)
        {
            _pessoaRepository =
                pessoaRepository
                ?? throw new ArgumentNullException(nameof(pessoaRepository));
            _abrigoRepository = 
                abrigoRepository 
                ?? throw new ArgumentNullException(nameof(abrigoRepository));
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

            var abrigosMencionados = new List<int>();

            foreach (var resultado in resultados)
            {
                if (!abrigosMencionados.Contains(resultado.AbrigoId))
                {
                    abrigosMencionados.Add(resultado.AbrigoId);
                }
            }

            var abrigos = (await _abrigoRepository.GetAbrigosPorIdsAsync(abrigosMencionados)).Select(s => new
            {
                s.Id,
                Nome = s.Nome.Value,
                Endereco = s.Endereco.ToString(),
            });

            var response = new
            {
                abrigos,
                pessoas = resultados.Select(p => new
                {
                    p.Id,
                    p.Nome.Value,
                    p.Idade,
                    p.InformacaoAdicional,
                    p.AbrigoId
                })
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
