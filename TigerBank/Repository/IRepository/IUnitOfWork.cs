namespace TigerBank.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IAccountTypeRepository AccountType { get; }
        IUsersRepository Users { get; }

        void Save();
    }
}
