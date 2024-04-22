using TodoModels.Models;
using TodoModels.Services;
using Microsoft.EntityFrameworkCore;
using TodoModels.DBContext;

namespace TodoModels.Repositories
{
    public class CredentialsRepository : GenericRepository<Credential>, ICredentials
    {
        public CredentialsRepository(TodoDbContext context) : base(context)
        {
        }

        public async Task<Credential> GetByUserId(Guid userId)
        {
            Credential credentials = await DbSet.Where(c => c.UserId == userId).FirstOrDefaultAsync();
            return credentials!;
        }
    }
}
