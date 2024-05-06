using System.IdentityModel.Tokens.Jwt;

namespace SOSRS.Api.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token =
                    context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                var jwtHandler = new JwtSecurityTokenHandler();
                var tokenClaims = jwtHandler.ReadJwtToken(token).Claims;

                var claimsDictionary = new Dictionary<string, string>();

                foreach (var claim in tokenClaims)
                {
                    claimsDictionary[claim.Type] = claim.Value;
                }

                context.Items["JwtClaims"] = claimsDictionary;
            }

            await _next(context);
        }
    }
}
