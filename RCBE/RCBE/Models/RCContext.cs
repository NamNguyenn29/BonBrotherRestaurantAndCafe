using Microsoft.EntityFrameworkCore;
using System.Data.Common;
namespace RCBE.Models
{
    public class RCContext : DbContext
    {

        public RCContext(DbContextOptions<RCContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Role> Role { get; set; }

    }
}
