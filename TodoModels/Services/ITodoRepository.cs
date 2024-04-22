using TodoModels.Models;

namespace TodoModels.Services
{
    public interface ITodoRepository : IGenericRepository<Todo>
    {

        Task<List<Todo>> GetUsersTodoAsync(Guid id);
    }
}
