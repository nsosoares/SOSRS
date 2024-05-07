namespace SOSRS.Api.ViewModels.Auth
{
    public class UserAuthJWTClaims
    {
        public Guid UserId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public List<int> UserAbrigosId { get; set; } = [];
    }
}
