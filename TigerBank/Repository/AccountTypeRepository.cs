using TigerBank.Models;
using TigerBank.Repository.IRepository;

namespace TigerBank.Repository
{
    public class AccountTypeRepository : Repository<AccountType>, IAccountTypeRepository
    {

        private readonly ApplicationDbContext _db;

        public AccountTypeRepository(ApplicationDbContext db) : base(db) //special note
        {
            _db = db;
        }

        public void Update(AccountType obj)
        {
            _db.AccountType.Update(obj);
        }
    }
}
