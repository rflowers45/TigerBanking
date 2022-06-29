using TigerBank.Models;

namespace TigerBank.Repository.IRepository
{
    public interface ITransactionRepository : IRepository<Transactions>
    {
        void Update(Transactions obj);
    }
}
