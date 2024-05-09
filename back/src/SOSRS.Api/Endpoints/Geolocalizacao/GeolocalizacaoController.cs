
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SOSRS.Api.Entities;

namespace SOSRS.Api.Endpoints.Geolocalizacao
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocalizacaoController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetAsync(string latitude, string longitude)
        {
            if (string.IsNullOrWhiteSpace(longitude) || string.IsNullOrWhiteSpace(latitude))
            {
                return BadRequest("Longitude e Latitude são obrigatórios");
            }

            var httpClient = new HttpClient();
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key";

            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var resultadoDTO = JsonConvert.DeserializeObject<GelocalizacaoResultadoDTO>(data);
                    var endereco = resultadoDTO.plus_code;
                    var cidade = endereco.compound_code.Split(",")[1].Trim();

                    var resultado = new GeolocalizacasoResultado()
                    {
                        cidade = cidade
                    };

                    return Ok(resultado);
                }
                else
                {
                    throw new Exception("Erro ao buscar dados de geolocalização, nao obtemos sucesso: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("erro ao fazer a chamada de geolocalização, dados: " + url, ex);
            }

        }
    }
}
