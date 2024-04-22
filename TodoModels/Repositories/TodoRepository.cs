using Microsoft.EntityFrameworkCore;
using TodoModels.Models;
using TodoModels.Services;
using TodoModels.DBContext;

namespace TodoModels.Repositories
{
    public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext context) : base(context)
        {
        }

        public override Task<List<Todo>> GetAllAsync()
        {
            return DbSet.OrderByDescending(t => t.CreatedDate).ToListAsync();
        }

        public override async Task<Todo> GetAsync(Guid id)
        {
            Todo todo = await DbSet.Where(t => t.TodoId == id).FirstOrDefaultAsync();
            return todo;
        }

        public async Task<List<Todo>> GetUsersTodoAsync(Guid id)
        {
            List<Todo> todo = await DbSet.Where(t => t.UserId == id).ToListAsync();
            return todo;
        }

        public override Task<bool> AddEntity(Todo entity)
        {
            try
            {
                DbSet.Add(entity);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

     
        public override Task<bool> UpdateEntity(Guid id, Todo entity)
        {
            try
            {
                var existingTodo = DbSet.Where(t => t.TodoId == id).FirstOrDefault();

                if (existingTodo != null)
                {

                    entity.Status = (Status)Enum.ToObject(typeof(Status), entity.Status);
                    entity.Priority = (Priority)Enum.ToObject(typeof(Priority), entity.Priority);

                    existingTodo.Title = entity.Title ?? existingTodo.Title;
                    existingTodo.Description = entity.Description ?? existingTodo.Description;
                    existingTodo.Status = entity.Status != null ? entity.Status : existingTodo.Status;
                    existingTodo.Priority = entity.Priority != null ? entity.Priority : existingTodo.Priority;
                    existingTodo.CreatedDate = entity.CreatedDate;
                    existingTodo.UpdatedDate = DateTime.Now;
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
