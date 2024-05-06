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

public static class AbrigoEndpoints
{
    public static void MapAbrigoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/abrigos", Get)
            .WithTags("Abrigos")
            .WithOpenApi();

        app.MapPost("api/abrigos", Post)
           .WithTags("Abrigos")
           .WithOpenApi();

        app.MapPut("api/abrigos/{id}", Put)
          .WithTags("Abrigos")
          .WithOpenApi();

        app.MapDelete("api/abrigos/{id}", Delete)
          .WithTags("Abrigos")
          .WithOpenApi();
    }

    private static async Task<IResult> Get(
        [AsParameters] FiltroAbrigoViewModel filtroAbrigoViewModel, 
        [FromServices] AppDbContext dbContext,
        HttpContext httpContext)
    {
        const int TEMPO_ARMAZENAMENTO_CACHE = 10;
        httpContext.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + TEMPO_ARMAZENAMENTO_CACHE;
        var abrigos = await dbContext.Abrigos
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

        return Results.Ok(new FiltroAbrigoResponseViewModel { Abrigos = abrigos!, QuantidadeTotalRegistros = dbContext.Abrigos.Count() });
    }

    private static async Task<IResult> Post(
        [FromBody] AbrigoRequestViewModel abrigoRequest, 
        [FromServices] AppDbContext dbContext,
        [FromServices] IValidadorService validadorService,
        [FromHeader(Name = "codAcesso")] int codAcesso)
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

        var result = validadorService.Validar(abrigo, new AbrigoValidador());
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors.Select(error => new ErrorResponseDTO { MensagemErro = error.ErrorMessage }));
        }

        await dbContext.AddAsync(abrigo);
        dbContext.Logs.Add(new Log(0, codAcesso, ETipoOperacao.Registrar, JsonConvert.SerializeObject(abrigoRequest)));
        await dbContext.SaveChangesAsync();
        return Results.Ok(abrigoRequest);
    }

    private static async Task<IResult> Put(
        [FromRoute] int id,
        [FromBody] AbrigoRequestViewModel abrigoRequest,
        [FromServices] AppDbContext dbContext,
        [FromServices] IValidadorService validadorService,
        [FromHeader(Name = "codAcesso")] int codAcesso)
    {
        var abrigoExiste = dbContext.Abrigos.Any(x => x.Id == id);
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

        var result = validadorService.Validar(abrigo, new AbrigoValidador());
        if (!result.IsValid)
        {
            return Results.BadRequest(result.Errors.Select(error => new ErrorResponseDTO { MensagemErro = error.ErrorMessage }));
        }

        var alimentosAnteriores = await dbContext.Alimentos.AsNoTracking().Where(x => x.AbrigoId == id && !abrigo.Alimentos.Select(x => x.Id).Contains(x.Id)).ToListAsync();
        dbContext.RemoveRange(alimentosAnteriores);
        dbContext.Update(abrigo);
        dbContext.Logs.Add(new Log(0, codAcesso, ETipoOperacao.Atualizar, JsonConvert.SerializeObject(abrigoRequest)));
        await dbContext.SaveChangesAsync();
        return Results.Ok(abrigoRequest);
    }

    private static async Task<IResult> Delete(
        [FromRoute] int id, 
        [FromServices] AppDbContext dbContext,
        [FromHeader(Name = "codAcesso")] int codAcesso)
    {
        var abrigo = await dbContext.Abrigos.FirstOrDefaultAsync(x => x.Id == id);
        if (abrigo == null)
        {
            return Results.NotFound();
        }

        dbContext.Remove(abrigo);
        dbContext.Logs.Add(new Log(0, codAcesso, ETipoOperacao.Deletar, JsonConvert.SerializeObject(abrigo)));
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
}
