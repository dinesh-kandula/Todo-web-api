using Microsoft.EntityFrameworkCore;

namespace MvcTodoApp.Models
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Blog> Blogs { get; set; } 
        
    }
}
