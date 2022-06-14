using Microsoft.EntityFrameworkCore;

namespace TigerBank.Models
{
    public class AuthDbContext : DbContext
    {
      public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {

        }
        public DbSet<Users> Users => Set<Users>();
        public DbSet<Accounts> Accounts => Set<Accounts>();
        public DbSet<Transactions> Transactions => Set<Transactions>();

    }
}
