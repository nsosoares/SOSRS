using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOSRS.Api.Data;
using SOSRS.Api.Entities;
using SOSRS.Api.Helpers;
using SOSRS.Api.Services;
using SOSRS.Api.Validations;
using SOSRS.Api.ValueObjects;

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
    }

    private static async Task<IResult> Get(
        [AsParameters] FiltroAbrigoViewModel filtroAbrigoViewModel, 
        [FromServices] AppDbContext dbContext)
    {
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
            .Select(x => new AbrigoResponseViewModel
            {
                Nome = x.Nome.Value,
                Cidade = x.Endereco.Cidade.Value,
                Bairro = x.Endereco.Bairro.Value,
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

    public class FiltroAbrigoResponseViewModel
    {
        public List<AbrigoResponseViewModel> Abrigos { get; set; } = default!;
        public int QuantidadeTotalRegistros { get; set; }
    }

    public class AbrigoResponseViewModel
    {
        public string Nome { get; set; } = default!;
        public string Cidade { get; set; } = default!;
        public string Bairro { get; set; } = default!;
        public EStatusCapacidade Capacidade { get; set; } = default!;
        public bool PrecisaAjudante { get; set; } = default!;
        public bool PrecisaAlimento { get; set; } = default!;
    }

    public enum EStatusCapacidade
    {
        Lotado,
        Disponivel
    }

    public class FiltroAbrigoViewModel
    {
        public string? Nome { get; set; } = default!;
        public string? Cidade { get; set; } = default!;
        public string? Bairro { get; set; } = default!;
        public string? Alimento { get; set; } = default!;
        public EFiltroStatusCapacidade? Capacidade { get; set; } = EFiltroStatusCapacidade.Todos;
        public bool? PrecisaAjudante { get; set; } = default!;
        public bool? PrecisaAlimento { get; set; } = default!;
    }

    public enum EFiltroStatusCapacidade
    {
        Lotado,
        Disponivel,
        Todos
    }

    private static async Task<IResult> Post(
        [FromBody] AbrigoRequestViewModel abrigoRequest, 
        [FromServices] AppDbContext dbContext,
        [FromServices] IValidadorService validadorService)
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

        dbContext.Add(abrigo);
        dbContext.SaveChanges();
        return Results.Ok(abrigoRequest);
    }
}

public class ErrorResponseDTO
{
    public string MensagemErro { get; set; } = default!;
}

public class AbrigoRequestViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = default!;
    public int? QuantidadeNecessariaVoluntarios { get; set; } = default!;
    public int? QuantidadeVagasDisponiveis { get; set; } = default!;
    public int? CapacidadeTotalPessoas { get; set; } = default!;
    public string TipoChavePix { get; set; } = default!;
    public string ChavePix { get; set; } = default!;
    public string Observacao { get; set; } = default!;
    public EnderecoViewModel Endereco { get; set; } = default!;
    public List<AlimentoViewModel> Alimentos { get; set; } = default!;
}

public class AlimentoViewModel
{
    public int Id { get; set; }
    public int AbrigoId { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public int? QuantidadeNecessaria { get; set; } = default!;
}

public class EnderecoViewModel
{
    public string Rua { get; set; } = default!;
    public int Numero { get; set; } = default!;
    public string Bairro { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Complemento { get; set; } = default!;
    public string Cep { get; set; } = default!;
}
