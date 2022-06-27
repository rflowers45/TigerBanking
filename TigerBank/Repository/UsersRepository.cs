using TigerBank.Models;
using TigerBank.Repository.IRepository;

namespace TigerBank.Repository
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        private readonly ApplicationDbContext _db;

        public UsersRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Users obj)
        {
            _db.Users.Update(obj);
        }
    }
}
