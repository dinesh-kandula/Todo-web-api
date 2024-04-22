using TodoModels.Services;
using TodoModels.DBContext;

namespace TodoModels.Repositories
{
    public class UnitofWorkRepository : IUnitofWork
    {
        private readonly TodoDbContext _context;

        public IUserRepository UserRepository { get; private set;}
        public ICredentials Credentials { get; private set; }
        public ITodoRepository TodoRepository { get; private set; }

        public UnitofWorkRepository(TodoDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(context);
            Credentials = new CredentialsRepository(context);
            TodoRepository = new TodoRepository(context);
        }

        public async Task CompleteAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
