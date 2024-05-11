using Microsoft.AspNetCore.Mvc;
using SOSRS.Api.Data;
using SOSRS.Api.Services.Authentication;
using SOSRS.Api.ViewModels.Auth;

namespace SOSRS.Api.Endpoints
{
    [ApiController]
    [Route("auth")]
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
                return NotFound("");
            }

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CadastrarUsuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Usuário precisa ser informado");
            }
            if (ValidarCpf(usuario.Cpf) == false)
            {
                return BadRequest("CPF inválido");
            }
            var response = await _authService.RegisterUserAsync(usuario.User, usuario.Password, usuario.Cpf, usuario.Telefone);

            if (response)
            {
                return Ok(response);
            }

            return BadRequest("Usuario já existe");
        }

        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
