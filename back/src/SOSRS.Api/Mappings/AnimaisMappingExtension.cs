using SOSRS.Api.Entities;
using SOSRS.Api.ViewModels.Animais;

namespace SOSRS.Api.Mappings
{
    public static class AnimaisMappingExtension
    {
        public static Animal MapToAnimal(this CreateAnimalRequest value)
        {
            var animal = new Animal
            {
                Nome = value.Nome,
                Tipo = value.Tipo,
                IdadeAproximada = value.IdadeAproximada,
                Raca = value.Raca,
                Cor = value.Cor,
                Peso = value.Peso,
                Genero = value.Genero,
                DataDeEntrada = value.DataDeEntrada,
                AbrigoId = value.AbrigoId,
            };

            return animal;
        }
        
        public static Animal MapToAnimal(this EditAnimalRequest value)
        {
            var animal = new Animal
            {
                Nome = value.Nome,
                Tipo = value.Tipo,
                IdadeAproximada = value.IdadeAproximada,
                Raca = value.Raca,
                Cor = value.Cor,
                Peso = value.Peso,
                Genero = value.Genero,
                DataDeEntrada = value.DataDeEntrada,
                AbrigoId = value.AbrigoId,
            }.WithId(value.Id);

            return animal;
        }


    }
}
