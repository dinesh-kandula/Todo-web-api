namespace TodoModels.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<bool> AddEntity(T entity);
        Task<bool> UpdateEntity(Guid id, T entity);
        Task<bool> DeleteEntity(T entity);
    }
}
