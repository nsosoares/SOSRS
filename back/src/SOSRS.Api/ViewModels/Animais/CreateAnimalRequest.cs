using SOSRS.Api.Enums;

namespace SOSRS.Api.ViewModels.Animais
{
    public class CreateAnimalRequest
    {
        public int AbrigoId { get; set; } 
        public string Nome { get; set; } = string.Empty;
        public TipoAnimalEnum Tipo { get; set; } = TipoAnimalEnum.Indefinido;
        public int? IdadeAproximada { get; set; }
        public string Raca { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public decimal? Peso { get; set; }
        public string Genero { get; set; } = string.Empty;
        public DateTime DataDeEntrada { get; set; } = DateTime.UtcNow;

    }
}
