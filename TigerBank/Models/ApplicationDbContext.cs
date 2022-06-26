using Microsoft.EntityFrameworkCore;

namespace TigerBank.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users => Set<Users>();

        public DbSet<Accounts> Accounts => Set<Accounts>();
        public DbSet<AccountType> AccountType => Set<AccountType>();
    }
}
