using TigerBank.Models;

namespace TigerBank.Repository.IRepository
{
    public interface IAccountTypeRepository : IRepository<AccountType>
    {
        void Update(AccountType obj);
    }
}
