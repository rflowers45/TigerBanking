using TigerBank.Models;

namespace TigerBank.Repository.IRepository
{
    public interface IUsersRepository : IRepository<User>
    {
        void Update(User obj);
    }
}
