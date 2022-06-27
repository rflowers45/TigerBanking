using TigerBank.Models;
using TigerBank.Repository.IRepository;

namespace TigerBank.Repository
{
    public class AccountsRepository : Repository<Accounts>, IAccountsRepository
    {
        private readonly ApplicationDbContext _db;

        public AccountsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Accounts obj)
        {
            var objFromDb = _db.Accounts.FirstOrDefault(u => u.UserId == obj.UserId);
            if(objFromDb != null)
            {
                objFromDb.AccountName = obj.AccountName;
                objFromDb.Balance = obj.Balance;
                objFromDb.UserId = obj.UserId;
                objFromDb.AccountTypeId = obj.AccountTypeId;
            }
        }
    }
}
