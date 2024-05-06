using SOSRS.Api.ViewModels.Auth;

namespace SOSRS.Api.Services.Authentication
{
    public interface IAuthService
    {
        Task<UserLoginResponse> SignInUserAsync(string email, string password);
        Task<bool> RegisterUserAsync(string email, string password, string cpf);
        string GenerateJWTToken(UserAuthJWTClaims username);
    }
}
