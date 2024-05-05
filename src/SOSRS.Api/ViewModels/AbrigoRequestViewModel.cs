
namespace SOSRS.Api.ViewModels;

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
