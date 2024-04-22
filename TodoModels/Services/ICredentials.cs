using Microsoft.EntityFrameworkCore;
using TodoModels.Models;

namespace TodoModels.Services
{
    public interface ICredentials : IGenericRepository<Credential>
    {
        Task<Credential> GetByUserId(Guid userId);
    }
    
}
