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
            Account = new AccountsRepository(_db);
            Transaction = new TransactionRepository(_db);

        }

        public IAccountTypeRepository AccountType { get; private set; }

        public IUsersRepository Users { get; private set; }

        public IAccountsRepository Account { get; private set; }

        public ITransactionRepository Transaction { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
