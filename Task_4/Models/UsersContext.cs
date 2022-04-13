using Microsoft.EntityFrameworkCore;
namespace Task_4.Models
{
    public class UsersContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        { 
            Database.EnsureCreated();
        }
    }
}
