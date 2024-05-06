namespace SOSRS.Api.ViewModels.Auth
{
    public class UserLoginRequest
    {
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
