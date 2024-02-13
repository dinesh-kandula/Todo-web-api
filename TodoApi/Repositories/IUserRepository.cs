using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
namespace TodoApi.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        //Task<User> GetUser(Guid id);
        //Task<User> AddUser(User user);
        //Task<User> UpdateUser(User user);
        //void DeleteUser(Guid id);
    }
}
