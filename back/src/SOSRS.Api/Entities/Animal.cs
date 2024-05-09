using SOSRS.Api.Enums;

namespace SOSRS.Api.Entities
{
    public class Animal : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public TipoAnimalEnum Tipo { get; set; } = TipoAnimalEnum.Indefinido;
        public int? IdadeAproximada { get; set; }
        public string Raca { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public decimal? Peso { get; set; }
        public string Genero { get; set; } = string.Empty;
        public DateTime DataDeEntrada { get; set; } = DateTime.UtcNow;

        public int AbrigoId { get; set; }
        public virtual Abrigo Abrigo { get; set; }

        public Animal WithId(int id)
        {
            var animal = this;
            animal.Id = id;

            return animal;
        }
    }
}
