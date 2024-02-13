using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TodoDbContext context) : base(context)
        {
        }


        public override async Task<List<User>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<User> GetAsync(Guid id)
        {
            User? user = await DbSet.Where(user => user.UserId == id).FirstOrDefaultAsync();
            return user;
        }

    }
}
