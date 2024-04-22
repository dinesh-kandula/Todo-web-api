using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoModels.Models;
using TodoModels.Services;
using TodoModels.DBContext;

namespace TodoModels.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TodoDbContext context) : base(context)
        {
        }

        public override async Task<User> GetAsync(Guid id)
        {
            User user = await DbSet.Where(user => user.UserId == id).FirstOrDefaultAsync();
            return user;
        }

        public override Task<bool> AddEntity(User entity)
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

        public override Task<bool> UpdateEntity(Guid id, User entity)
        {
            try
            {
                var existingUser = DbSet.Where(user => user.UserId == id).FirstOrDefault();

                if(existingUser != null)
                {
                    if(entity.Gender != null)
                    {
                        entity.Gender = (Gender)Enum.ToObject(typeof(Gender), entity.Gender);
                    }

                    existingUser.Name = entity.Name ?? existingUser.Name;
                    existingUser.Gender = entity.Gender ?? existingUser.Gender;
                    existingUser.DOB = entity.DOB ?? existingUser.DOB;
                    existingUser.Profile = entity.Profile ?? existingUser.Profile;
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
