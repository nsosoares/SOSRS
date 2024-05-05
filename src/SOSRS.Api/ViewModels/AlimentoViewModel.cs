
namespace SOSRS.Api.ViewModels;

public class AlimentoViewModel
{
    public int Id { get; set; }
    public int AbrigoId { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public int? QuantidadeNecessaria { get; set; } = default!;
}
