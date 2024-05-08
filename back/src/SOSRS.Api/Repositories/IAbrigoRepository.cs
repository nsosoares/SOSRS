using SOSRS.Api.Entities;
using SOSRS.Api.ViewModels;

namespace SOSRS.Api.Repositories
{
    public interface IAbrigoRepository
    {
        Task<Abrigo?> GetAbrigoPorIdAsync(int id);
        Task<List<Abrigo>> GetAbrigosPorIdsAsync(List<int> ids);
        Task<List<AbrigoResponseViewModel>> GetAbrigos(FiltroAbrigoViewModel filtroAbrigoViewModel);
    }
}
