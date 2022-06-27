using Microsoft.EntityFrameworkCore;
using TigerBank.Models;

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
        public DbSet<AccountType> AccountType => Set<AccountType>();

    }
  
}

