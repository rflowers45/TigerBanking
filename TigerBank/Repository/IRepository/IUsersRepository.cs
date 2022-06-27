using TigerBank.Models;

namespace TigerBank.Repository.IRepository
{
    public interface IUsersRepository : IRepository<Users>
    {
        void Update(Users obj);
    }
}
