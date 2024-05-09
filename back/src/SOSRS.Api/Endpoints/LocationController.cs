using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SOSRS.Api.ViewModels.Location;

namespace SOSRS.Api.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetLocation(string latitude, string longitude)
        {
            var apiUrl = $"https://api.opencagedata.com/geocode/v1/json?key=b6d741d3a59548f48071226ee27bf0fc&q={latitude}%2C{longitude}&pretty=1&no_annotations=1";

            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(apiUrl);
            var objectResponse = JsonConvert.DeserializeObject<LocationApiResponse>(response);

            return Ok(new
            {
                Location = objectResponse?.results?.FirstOrDefault()?.components
            });
        }
    }
}
