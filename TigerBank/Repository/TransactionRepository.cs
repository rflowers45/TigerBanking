using TigerBank.Models;
using TigerBank.Repository.IRepository;

namespace TigerBank.Repository
{
    public class TransactionRepository : Repository<Transactions>, ITransactionRepository
    {
        private readonly ApplicationDbContext _db;

        public TransactionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Transactions obj)
        {
            _db.Transactions.Update(obj);
        }
    }
}
