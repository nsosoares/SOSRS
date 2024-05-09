using SOSRS.Api.Endpoints.Geolocalizacao;

namespace TestesIntegracao
{
    public class GeolocalizaçãoTests
    {

        [Theory]
        //[InlineData("-29.684274", "-51.150761")]
        [InlineData("-29.680397", "-51.132951", "Novo Hamburgo")]
        [InlineData("-29.676556", "-51.149173", "Novo Hamburgo")]
        [InlineData("-29.648662", "-51.179643", "Estância Velha")]

        public async Task DeveRetornarListaDeAbrigos(string latitude, string longitude, string cidade)
        {
            var serviceProvider = Injections.GetProviders();
            var controller = new GeolocalizacaoController();
            var resultado  = await controller.GetAsync(latitude, longitude);

            //Assert
        }
    }
}
