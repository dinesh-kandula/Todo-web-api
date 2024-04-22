using Microsoft.AspNetCore.Mvc;
using TodoModels.Models;

namespace TodoModels.Services
{
    public interface IUserRepository : IGenericRepository<User>
    {

    }
}
