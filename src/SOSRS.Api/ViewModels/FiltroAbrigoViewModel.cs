namespace SOSRS.Api.ViewModels;

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
