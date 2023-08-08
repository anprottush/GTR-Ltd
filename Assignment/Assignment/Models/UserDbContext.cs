using Microsoft.EntityFrameworkCore;

namespace Assignment.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Document> Documents { get; set; }

    }
}
