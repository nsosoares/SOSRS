
namespace SOSRS.Api.ViewModels;

public class PessoaDesaparecidaViewModel
{
    public int Id { get; set; }
    public int AbrigoId { get; set; } = default!;
    public Guid AbrigoGuid { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public int? Idade { get; set; } = default!;
    public string InformacaoAdicional { get; set; } = default!;
    public IFormFile? Foto { get; set; } = default!;
}
