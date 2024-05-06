namespace SOSRS.Api.ViewModels.Auth
{
    public class UserLoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; } = DateTime.UtcNow.AddHours(1).AddMinutes(-3);
        public UserAuthJWTClaims UserInfo { get; set; } = null!;
    }
}
