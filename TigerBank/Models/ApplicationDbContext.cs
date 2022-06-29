using Microsoft.EntityFrameworkCore;

namespace TigerBank.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Accounts> Accounts => Set<Accounts>();
        public DbSet<AccountType> AccountType => Set<AccountType>();

        public DbSet<Transactions> Transactions => Set<Transactions>();
    }
}
