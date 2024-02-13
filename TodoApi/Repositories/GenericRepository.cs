
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TodoDbContext _context;

        internal DbSet<T> DbSet { get; set; }

        public GenericRepository(TodoDbContext context)
        {
            _context = context;
            this.DbSet = _context.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await this.DbSet.ToListAsync();
        }

        public virtual Task<T> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateEntity(T entity)
        {
            throw new NotImplementedException();
        }
        public virtual Task<bool> AddEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteEntity(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
