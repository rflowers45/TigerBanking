using TigerBank.Models;

namespace TigerBank.Repository.IRepository
{
    public interface IAccountsRepository : IRepository<Accounts> 
    {
        void Update(Accounts obj);      
    }
}
