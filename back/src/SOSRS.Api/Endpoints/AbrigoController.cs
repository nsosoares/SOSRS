using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using SOSRS.Api.Data;
using SOSRS.Api.Entities;
using SOSRS.Api.Extensions;
using SOSRS.Api.Repositories;
using SOSRS.Api.Services;
using SOSRS.Api.Validations;
using SOSRS.Api.ValueObjects;
using SOSRS.Api.ViewModels;
using SOSRS.Api.ViewModels.Abrigo;
using SOSRS.Api.ViewModels.Location;
using System.Web;

namespace SOSRS.Api.Endpoints;

[Route("api/abrigos")]
[ApiController]
public class AbrigoController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IValidadorService _validadorService;
    private readonly IAbrigoRepository _abrigoRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public AbrigoController(AppDbContext dbContext, IHttpContextAccessor httpContext, IValidadorService validadorService, IAbrigoRepository abrigoRepository, IPessoaRepository pessoaRepository)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        _validadorService = validadorService ?? throw new ArgumentNullException(nameof(validadorService));
        _abrigoRepository = abrigoRepository ?? throw new ArgumentNullException(nameof(abrigoRepository));
        _pessoaRepository = pessoaRepository ?? throw new ArgumentNullException(nameof(pessoaRepository));
    }

    [HttpGet("version")]
    public IResult GetVersion()
    {
        return Results.Ok(new { version = "7" });
    }

    [HttpGet()]
    public async Task<IResult> Get([FromQuery] FiltroAbrigoViewModel filtroAbrigoViewModel)
    {
        return await GetAbrigos(filtroAbrigoViewModel);
    }

    [HttpGet("GetByUserId")]
    [Authorize]
    public async Task<IResult> GetByUserId([FromQuery] FiltroAbrigoViewModel filtroAbrigoViewModel)
    {
        var usuarioId = HttpContext.GetUsuarioId();
        return await GetAbrigos(filtroAbrigoViewModel, usuarioId);
    }

    private async Task<IResult> GetAbrigos(FiltroAbrigoViewModel filtroAbrigoViewModel, Guid? usuarioId = null)
    {
        const int TEMPO_ARMAZENAMENTO_CACHE = 10;
        _httpContext.HttpContext!.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + TEMPO_ARMAZENAMENTO_CACHE;
        var abrigos = await _abrigoRepository.GetAbrigos(filtroAbrigoViewModel);

        if (abrigos == null)
        {
            Results.NotFound();
        }

        return Results.Ok(new FiltroAbrigoResponseViewModel { Abrigos = abrigos!, QuantidadeTotalRegistros = _dbContext.Abrigos.Count() });
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetById([FromRoute] int id)
    {
        var abrigo = await _dbContext.Abrigos
            .Select(x => new AbrigoRequestViewModel
            {
                Id = x.Id,
                Nome = x.Nome.Value,
                ChavePix = x.ChavePix,
                CapacidadeTotalPessoas = x.CapacidadeTotalPessoas,
                Observacao = x.Observacao,
                QuantidadeNecessariaVoluntarios = x.QuantidadeNecessariaVoluntarios,
                TipoChavePix = x.TipoChavePix,
                QuantidadeVagasDisponiveis = x.QuantidadeVagasDisponiveis,
                TipoAbrigo = x.TipoAbrigo,
                
                Telefone = x.Telefone,
                Endereco = new EnderecoViewModel
                {
                    Rua = x.Endereco.Rua.Value,
                    Cep = x.Endereco.Cep,
                    Cidade = x.Endereco.Cidade.Value,
                    Complemento = x.Endereco.Complemento,
                    Bairro = x.Endereco.Bairro.Value,
                    Numero = x.Endereco.Numero,
                },
                Alimentos = x.Alimentos.Select(a => new AlimentoViewModel
                {
                    Id = a.Id,
                    AbrigoId = a.AbrigoId,
                    QuantidadeNecessaria = a.QuantidadeNecessaria,
                    Nome = a.Nome.Value
                }).ToList(),
                PessoasDesaparecidas = x.PessoasDesaparecidas.Select(p => new PessoaDesaparecidaViewModel
                {
                    Id = p.Id,
                    AbrigoId = p.AbrigoId,
                    Idade = p.Idade,
                    InformacaoAdicional = p.InformacaoAdicional,
                    Nome = p.Nome.Value
                }).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        return abrigo is not null
            ? Results.Ok(abrigo)
            : Results.NotFound();
    }

    [Authorize]
    [HttpPost]
    public async Task<IResult> Post([FromBody] AbrigoRequestViewModel abrigoRequest)
    {
        
        var usuarioId = HttpContext.GetUsuarioId();

        var endereco = new EnderecoVO(
            abrigoRequest.Endereco.Rua,
            abrigoRequest.Endereco.Numero,
            abrigoRequest.Endereco.Bairro,
            abrigoRequest.Endereco.Cidade,
            "RS",
            abrigoRequest.Endereco.Complemento,
            abrigoRequest.Endereco.Cep);

        var alimentos = abrigoRequest.Alimentos == null ? new List<Alimento>()
            : abrigoRequest.Alimentos.Select(x => new Alimento(x.Id, x.AbrigoId, x.Nome, x.QuantidadeNecessaria)).ToList();

        var pessoasDesaparecidas = abrigoRequest.PessoasDesaparecidas == null ? new List<PessoaDesaparecida>()
            : abrigoRequest.PessoasDesaparecidas.Select(x => new PessoaDesaparecida(x.Id, x.AbrigoId, x.Nome, x.Idade, x.InformacaoAdicional, x.Foto)).ToList();

        var (latitude, longitude) = await GetCoordinates(endereco.ToString());

        var abrigo = new Abrigo(
            abrigoRequest.Id,
            abrigoRequest.Nome,
            abrigoRequest.QuantidadeNecessariaVoluntarios,
            abrigoRequest.QuantidadeVagasDisponiveis,
            abrigoRequest.CapacidadeTotalPessoas,
            abrigoRequest.TipoChavePix,
            abrigoRequest.ChavePix,
            abrigoRequest.Telefone,
            abrigoRequest.Observacao ?? "",
            
            latitude,
            longitude,

            usuarioId,
            endereco,
            alimentos,
            pessoasDesaparecidas,
            abrigoRequest.TipoAbrigo
            );

        var result = _validadorService.Validar(abrigo, new AbrigoValidador());
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors.Select(error => new ErrorResponseDTO { MensagemErro = error.ErrorMessage }));
        }

        await _dbContext.AddAsync(abrigo);
        _dbContext.Logs.Add(new Log(0, usuarioId, ETipoOperacao.Registrar, JsonConvert.SerializeObject(abrigoRequest)));
        await _dbContext.SaveChangesAsync();
        return Results.Ok(abrigoRequest);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IResult> Put([FromRoute] int id, [FromBody] AbrigoRequestViewModel abrigoRequest)
    {
        var usuarioId = HttpContext.GetUsuarioId();
        var abrigoExiste = _dbContext.Abrigos.Any(x => x.Id == id);
        if (!abrigoExiste)
        {
            return Results.NotFound();
        }

        var endereco = new EnderecoVO(
            abrigoRequest.Endereco.Rua ?? "",
            abrigoRequest.Endereco.Numero,
            abrigoRequest.Endereco.Bairro ?? "",
            abrigoRequest.Endereco.Cidade,
            "RS",
            abrigoRequest.Endereco.Complemento ?? "",
            abrigoRequest.Endereco.Cep ?? "");

        var alimentos = abrigoRequest.Alimentos == null ? new List<Alimento>()
            : abrigoRequest.Alimentos.Select(x => new Alimento(x.Id, x.AbrigoId, x.Nome, x.QuantidadeNecessaria)).ToList();

        var pessoasDesaparecidas = abrigoRequest.PessoasDesaparecidas == null ? new List<PessoaDesaparecida>()
            : abrigoRequest.PessoasDesaparecidas.Select(x => new PessoaDesaparecida(x.Id, x.AbrigoId, x.Nome, x.Idade, x.InformacaoAdicional, x.Foto)).ToList();

        var (latitude, longitude) = await GetCoordinates(endereco.ToString());

        var abrigo = new Abrigo(
            id,
            abrigoRequest.Nome,
            abrigoRequest.QuantidadeNecessariaVoluntarios,
            abrigoRequest.QuantidadeVagasDisponiveis,
            abrigoRequest.CapacidadeTotalPessoas,
            abrigoRequest.TipoChavePix ?? "",
            abrigoRequest.ChavePix ?? "",
            abrigoRequest.Telefone ?? "",
            abrigoRequest.Observacao ?? "",
            latitude, 
            longitude,

            usuarioId,
            endereco,
            alimentos,
            pessoasDesaparecidas,
            abrigoRequest.TipoAbrigo);

        var result = _validadorService.Validar(abrigo, new AbrigoValidador());
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors.Select(error => new ErrorResponseDTO { MensagemErro = error.ErrorMessage }));
        }

        var alimentosAnteriores = await _dbContext.Alimentos.AsNoTracking().Where(x => x.AbrigoId == id && !abrigo.Alimentos.Select(x => x.Id).Contains(x.Id)).ToListAsync();
        _dbContext.RemoveRange(alimentosAnteriores);
        _dbContext.Update(abrigo);
        //_dbContext.Logs.Add(new Log(0, usuarioId, ETipoOperacao.Atualizar, JsonConvert.SerializeObject(abrigoRequest)));
        await _dbContext.SaveChangesAsync();
        return Results.Ok(abrigoRequest);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IResult> Delete([FromRoute] int id)
    {
        var usuarioId = HttpContext.GetUsuarioId();
        var abrigo = await _dbContext.Abrigos.FirstOrDefaultAsync(x => x.Id == id && x.UsuarioId == usuarioId);
        if (abrigo == null)
        {
            return Results.NotFound();
        }

        _dbContext.Remove(abrigo);
        //_dbContext.Logs.Add(new Log(0, usuarioId, ETipoOperacao.Deletar, JsonConvert.SerializeObject(abrigo)));
        await _dbContext.SaveChangesAsync();
        return Results.Ok();
    }

    [Authorize]
    [HttpPost("relocate")]
    public async Task<IActionResult> RealocarPessoasDeAbrigo([FromBody] RelocatePeopleRequest relocatePeopleRequest)
    {
        var abrigoOrigem = await _abrigoRepository.GetAbrigoPorIdAsync(relocatePeopleRequest.AbrigoOrigem);
        var abrigoDestino = await _abrigoRepository.GetAbrigoPorIdAsync(relocatePeopleRequest.AbrigoDestino);

        var quantidadePessoasAbrigoOrigem = abrigoOrigem?.CapacidadeTotalPessoas - abrigoOrigem?.QuantidadeVagasDisponiveis;

        if(quantidadePessoasAbrigoOrigem < abrigoDestino?.CapacidadeTotalPessoas)
        {
            return StatusCode(406, new { message = "O abrigo de destino não tem capacidade para relocação deste abrigo" });
        }

        var isPessoasRealocadas = await _pessoaRepository.RealocarPessoasDeAbrigo(relocatePeopleRequest.AbrigoOrigem, relocatePeopleRequest.AbrigoDestino);

        if(isPessoasRealocadas)
        {
            return Ok();
        }

        return StatusCode(500, new { Message = "Falha ao realocar pessoas de abrigo." });
    }

    private async Task<(string, string)> GetCoordinates(string address)
    {
        var apiUrl = $"https://api.opencagedata.com/geocode/v1/json?q={HttpUtility.UrlEncode(address)}&key=b6d741d3a59548f48071226ee27bf0fc";

        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(apiUrl);
        var objectResponse = JsonConvert.DeserializeObject<LocationApiResponse>(response);

        var results = objectResponse?.results?.FirstOrDefault();

        if(results == null)
        {
            return ("", "");
        }

        var latitude = (results?.geometry?.lat).ToString() ?? "";
        var longitude = (results?.geometry?.lng).ToString() ?? "";

        return (latitude, longitude);
    }
}
