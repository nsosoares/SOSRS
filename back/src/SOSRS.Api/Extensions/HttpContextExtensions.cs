namespace SOSRS.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static List<Guid> GetAbrigos(this HttpContext context)
        {
            var claims = (IDictionary<string, string>?)context.Items["JwtClaims"];

            if (claims != null && !string.IsNullOrEmpty(claims["UserAbrigosId"]))
            {
                List<Guid> listaAbrigos = [];

                var abrigos = claims["UserAbrigosId"].Split('|');

                foreach(var abrigoGuid in abrigos)
                {
                    var abrigoId = Guid.Parse(abrigoGuid);

                    listaAbrigos.Add(abrigoId);
                }
                
                return listaAbrigos;
            }

            return [];
        }
    }
}
