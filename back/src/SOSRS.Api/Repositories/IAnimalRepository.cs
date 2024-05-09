using SOSRS.Api.Entities;

namespace SOSRS.Api.Repositories
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        Task<IReadOnlyCollection<Animal>> GetAnimalPorAbrigo(int abrigoId);
    }
}
