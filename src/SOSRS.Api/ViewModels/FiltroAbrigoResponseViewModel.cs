
namespace SOSRS.Api.ViewModels;

public class FiltroAbrigoResponseViewModel
{
    public List<AbrigoResponseViewModel> Abrigos { get; set; } = default!;
    public int QuantidadeTotalRegistros { get; set; }
}

public class AbrigoResponseViewModel
{
    public int Codigo { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Bairro { get; set; } = default!;
    public int Numero { get; set; } = default!;
    public string Complemento { get; set; } = default!;
    public string TipoChavePix { get; set; } = default!;
    public string ChavePix { get; set; } = default!;
    public EStatusCapacidade Capacidade { get; set; } = default!;
    public bool PrecisaAjudante { get; set; } = default!;
    public bool PrecisaAlimento { get; set; } = default!;
}

public enum EStatusCapacidade
{
    Lotado,
    Disponivel
}