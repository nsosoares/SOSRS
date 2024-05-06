using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SOSRS.Api.Configuration;
using SOSRS.Api.ViewModels.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SOSRS.Api.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly JWTConfiguration _configuration;

        public AuthService(JWTConfiguration configuration)
        {
            _configuration =
                configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateJWTToken(UserAuthJWTClaims userClaims)
        {
            var claims = new[]
            {
                new Claim("id", userClaims.UserId.ToString()),
                new Claim("nome", userClaims.Nome),
                new Claim("email", userClaims.Email),
                new Claim("cpf", userClaims.CPF),
                new Claim("abrigos", string.Join('|', userClaims.UserAbrigosId))
            };

            var token = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
                signingCredentials:
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                        SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<bool> RegisterUserAsync(string email, string password, string cpf)
        {
            throw new NotImplementedException();
        }

        public async Task<UserLoginResponse> SignInUserAsync(string email, string password)
        {
            await Task.CompletedTask;

            var user = new UserAuthJWTClaims
            {
                UserId = Guid.NewGuid(),
                Nome = "Teste",
                Email = "teste@teste.com.br",
                CPF = "999.999.999-99"
            };

            if (user.UserId != Guid.Empty)
            {
                var token = GenerateJWTToken(user);
                var response = new UserLoginResponse()
                {
                    Token = token,
                    UserInfo = new UserAuthJWTClaims
                    {
                        UserId = Guid.NewGuid(),
                        Nome = user.Nome,
                        Email = user.Email,
                        CPF = user.CPF,
                        UserAbrigosId = [Guid.NewGuid()]
                    }
                };

                return response;
            }
            else
            {
                return new UserLoginResponse();
            }

            throw new NotImplementedException();
        }
    }
}
