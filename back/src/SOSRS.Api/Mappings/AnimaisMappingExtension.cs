using SOSRS.Api.Entities;
using SOSRS.Api.ViewModels.Animais;

namespace SOSRS.Api.Mappings
{
    public static class AnimaisMappingExtension
    {
        public static Animal MapToAnimal(this CreateAnimalRequest value)
        {
            return new Animal();
        }
    }
}
