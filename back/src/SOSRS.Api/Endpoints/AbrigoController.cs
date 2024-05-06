using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using SOSRS.Api.Data;
using SOSRS.Api.Entities;
using SOSRS.Api.Helpers;
using SOSRS.Api.Services;
using SOSRS.Api.Validations;
using SOSRS.Api.ValueObjects;
using SOSRS.Api.ViewModels;

namespace SOSRS.Api.Endpoints;

[Route("api/abrigos")]
[ApiController]
public class AbrigoController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IValidadorService _validadorService;

    public AbrigoController(AppDbContext dbContext, IHttpContextAccessor httpContext, IValidadorService validadorService)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
        _validadorService = validadorService;
    }

    [HttpGet()]
    public async Task<IResult> Get([FromQuery] FiltroAbrigoViewModel filtroAbrigoViewModel)
    {
        const int TEMPO_ARMAZENAMENTO_CACHE = 10;
        _httpContext.HttpContext!.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + TEMPO_ARMAZENAMENTO_CACHE;
        var abrigos = await _dbContext.Abrigos
            .When(!string.IsNullOrEmpty(filtroAbrigoViewModel.Nome) && !string.IsNullOrWhiteSpace(filtroAbrigoViewModel.Nome)
                , x => x.Nome.SearchableValue.Contains(filtroAbrigoViewModel.Nome!.ToSerachable()))
            .When(!string.IsNullOrEmpty(filtroAbrigoViewModel.Cidade) && !string.IsNullOrWhiteSpace(filtroAbrigoViewModel.Cidade)
                , x => x.Endereco.Cidade.SearchableValue.Contains(filtroAbrigoViewModel.Cidade!.ToSerachable()))
            .When(!string.IsNullOrEmpty(filtroAbrigoViewModel.Bairro) && !string.IsNullOrWhiteSpace(filtroAbrigoViewModel.Bairro)
                , x => x.Endereco.Bairro.SearchableValue.Contains(filtroAbrigoViewModel.Bairro!.ToSerachable()))
            .When(filtroAbrigoViewModel.Capacidade != EFiltroStatusCapacidade.Todos && filtroAbrigoViewModel.Capacidade.HasValue
                , x => (filtroAbrigoViewModel.Capacidade == EFiltroStatusCapacidade.Lotado && x.Lotado)
                    || (filtroAbrigoViewModel.Capacidade == EFiltroStatusCapacidade.Disponivel && !x.Lotado))
            .When(filtroAbrigoViewModel.PrecisaAlimento.HasValue
                , x => (x.Alimentos == null || x.Alimentos.Count == 0) == filtroAbrigoViewModel.PrecisaAlimento)
            .When(filtroAbrigoViewModel.PrecisaAjudante.HasValue
                , x => (x.QuantidadeNecessariaVoluntarios.HasValue && x.QuantidadeNecessariaVoluntarios > 0) == filtroAbrigoViewModel.PrecisaAjudante)
            .When(!string.IsNullOrEmpty(filtroAbrigoViewModel.Alimento) && !string.IsNullOrWhiteSpace(filtroAbrigoViewModel.Alimento)
                , x => !x.Alimentos.Any(a => a.Nome.SearchableValue.Contains(filtroAbrigoViewModel.Alimento!.ToSerachable())))
            .Select(x => new AbrigoResponseViewModel
            {
                Id = x.Id,
                Nome = x.Nome.Value,
                Cidade = x.Endereco.Cidade.Value,
                Bairro = x.Endereco.Bairro.Value,
                Numero = x.Endereco.Numero,
                Complemento = x.Endereco.Complemento,
                TipoChavePix = x.TipoChavePix,
                ChavePix = x.ChavePix,
                Capacidade = x.Lotado ? EStatusCapacidade.Lotado : EStatusCapacidade.Disponivel,
                PrecisaAjudante = (x.QuantidadeNecessariaVoluntarios.HasValue && x.QuantidadeNecessariaVoluntarios > 0),
                PrecisaAlimento = (x.Alimentos == null || x.Alimentos.Count == 0)
            })
        .ToListAsync();
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
                }).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        return abrigo is not null
            ? Results.Ok(abrigo)
            : Results.NotFound();
    }

    [HttpPost]
    public async Task<IResult> Post([FromBody] AbrigoRequestViewModel abrigoRequest, [FromHeader(Name = "codAcesso")] int codAcesso)
    {
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

        var abrigo = new Abrigo(
            abrigoRequest.Id,
            abrigoRequest.Nome,
            abrigoRequest.QuantidadeNecessariaVoluntarios,
            abrigoRequest.QuantidadeVagasDisponiveis,
            abrigoRequest.CapacidadeTotalPessoas,
            abrigoRequest.TipoChavePix,
            abrigoRequest.ChavePix,
            abrigoRequest.Observacao,
            endereco,
            alimentos);

        var result = _validadorService.Validar(abrigo, new AbrigoValidador());
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors.Select(error => new ErrorResponseDTO { MensagemErro = error.ErrorMessage }));
        }

        await _dbContext.AddAsync(abrigo);
        _dbContext.Logs.Add(new Log(0, codAcesso, ETipoOperacao.Registrar, JsonConvert.SerializeObject(abrigoRequest)));
        await _dbContext.SaveChangesAsync();
        return Results.Ok(abrigoRequest);
    }

    [HttpPut("{id:int}")]
    public async Task<IResult> Put([FromRoute] int id, [FromBody] AbrigoRequestViewModel abrigoRequest, [FromHeader(Name = "codAcesso")] int codAcesso)
    {
        var abrigoExiste = _dbContext.Abrigos.Any(x => x.Id == id);
        if (!abrigoExiste)
        {
            return Results.NotFound();
        }

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

        var abrigo = new Abrigo(
            id,
            abrigoRequest.Nome,
            abrigoRequest.QuantidadeNecessariaVoluntarios,
            abrigoRequest.QuantidadeVagasDisponiveis,
            abrigoRequest.CapacidadeTotalPessoas,
            abrigoRequest.TipoChavePix,
            abrigoRequest.ChavePix,
            abrigoRequest.Observacao,
            endereco,
            alimentos);

        var result = _validadorService.Validar(abrigo, new AbrigoValidador());
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors.Select(error => new ErrorResponseDTO { MensagemErro = error.ErrorMessage }));
        }

        var alimentosAnteriores = await _dbContext.Alimentos.AsNoTracking().Where(x => x.AbrigoId == id && !abrigo.Alimentos.Select(x => x.Id).Contains(x.Id)).ToListAsync();
        _dbContext.RemoveRange(alimentosAnteriores);
        _dbContext.Update(abrigo);
        _dbContext.Logs.Add(new Log(0, codAcesso, ETipoOperacao.Atualizar, JsonConvert.SerializeObject(abrigoRequest)));
        await _dbContext.SaveChangesAsync();
        return Results.Ok(abrigoRequest);
    }

    [HttpDelete]
    public async Task<IResult> Delete([FromRoute] int id, [FromHeader(Name = "codAcesso")] int codAcesso)
    {
        var abrigo = await _dbContext.Abrigos.FirstOrDefaultAsync(x => x.Id == id);
        if (abrigo == null)
        {
            return Results.NotFound();
        }

        _dbContext.Remove(abrigo);
        _dbContext.Logs.Add(new Log(0, codAcesso, ETipoOperacao.Deletar, JsonConvert.SerializeObject(abrigo)));
        await _dbContext.SaveChangesAsync();
        return Results.Ok();
    }
}
