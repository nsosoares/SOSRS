namespace SOSRS.Api.ViewModels.Auth
{
    public class UserAuthJWTClaims
    {
        public Guid UserId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public List<Guid> UserAbrigosId { get; set; } = [];
    }
}
