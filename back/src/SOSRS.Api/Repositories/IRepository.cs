namespace SOSRS.Api.Repositories
{
    public interface IRepository<T>
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> EditAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Guid id);
    }
}
