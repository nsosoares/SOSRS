
namespace SOSRS.Api.ViewModels;

public class EnderecoViewModel
{
    public string Rua { get; set; } = default!;
    public int? Numero { get; set; } = default!;
    public string Bairro { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Complemento { get; set; } = default!;
    public string Cep { get; set; } = default!;
}