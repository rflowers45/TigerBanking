namespace TigerBank.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IAccountTypeRepository AccountType { get; }
        IUsersRepository Users { get; }
        
        IAccountsRepository Account { get; }

        ITransactionRepository Transaction { get; }

        void Save();
    }
}
