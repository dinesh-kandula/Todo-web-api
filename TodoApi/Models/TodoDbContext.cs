using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        { 
        }
        
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }  
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<FavBook> FavBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
               .Property(u => u.Gender)
               .HasConversion<string>();

            modelBuilder.Entity<Todo>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Todo>()
                .Property(t => t.Priority)
                .HasConversion<string>();

            modelBuilder.Entity<Credential>()
                .HasIndex(c => new { c.Username, c.EmailId })
                .IsUnique(true);
        }

    }
}
