using Microsoft.AspNetCore.Mvc;
using SOSRS.Api.Services.Authentication;
using SOSRS.Api.ViewModels.Auth;

namespace SOSRS.Api.Endpoints
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.User))
            {
                return BadRequest("Usuário precisa ser informado");
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Senha precisa ser informada");
            }

            var response = await _authService.SignInUserAsync(user.User, user.Password);

            if (string.IsNullOrEmpty(response.Token))
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] string user, string password, string cpf)
        {
            var response = await _authService.RegisterUserAsync(user, password, cpf);

            if (response)
            {
                return Ok(response);
            }

            return NotFound();
        }
    }
}
