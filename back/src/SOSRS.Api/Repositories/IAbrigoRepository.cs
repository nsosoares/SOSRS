using SOSRS.Api.Entities;
using SOSRS.Api.ViewModels;

namespace SOSRS.Api.Repositories
{
    public interface IAbrigoRepository
    {
        Task<List<Abrigo>> GetAbrigosPorIdsAsync(List<int> ids);
        Task<List<AbrigoResponseViewModel>> GetAbrigos(FiltroAbrigoViewModel filtroAbrigoViewModel, Guid? usuarioId = null);
    }
}
