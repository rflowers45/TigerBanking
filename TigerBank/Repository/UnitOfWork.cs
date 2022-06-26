using TigerBank.Models;
using TigerBank.Repository.IRepository;

namespace TigerBank.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Users = new UsersRepository(_db);
            AccountType = new AccountTypeRepository(_db);

        }

        public IAccountTypeRepository AccountType { get; private set; }

        public IUsersRepository Users { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
