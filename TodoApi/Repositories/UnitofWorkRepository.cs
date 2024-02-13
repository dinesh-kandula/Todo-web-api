
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class UnitofWorkRepository : IUnitofWork
    {
        private readonly TodoDbContext _context;

        public IUserRepository UserRepository { get; private set;}

        public UnitofWorkRepository(TodoDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(context);
        }

        public async Task CompleteAsync()
        {
           await this._context.SaveChangesAsync();
        }
    }
}
