namespace E_commerceTask.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{

    Task<int> CompleteAsync();
    int Complete();

    #region Transactions
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    #endregion
}
