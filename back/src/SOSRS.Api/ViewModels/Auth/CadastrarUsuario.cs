namespace SOSRS.Api.ViewModels.Auth
{
    public class CadastrarUsuario
    {
        public string User { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Cpf { get; set; } = default!;
        public string Telefone { get; set; } = default!;
    }
}
