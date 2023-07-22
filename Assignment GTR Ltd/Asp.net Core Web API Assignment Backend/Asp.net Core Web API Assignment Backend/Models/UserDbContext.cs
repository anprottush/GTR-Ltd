using Microsoft.EntityFrameworkCore;

namespace Asp.net_Core_Web_API_Assignment_Backend.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductMaster> ProductMasters { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
    }
}
