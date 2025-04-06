using E_commerceTask.Application.Interfaces.Customers;
using E_commerceTask.Application.Interfaces.Orders;

namespace E_commerceTask.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IOrderRepository Order { get; }
    public IOrderProductRepository OrderProduct { get; }    
    public IProductRepository Product { get; }
    public ICustomerRepository Customer { get; }
    Task<int> CompleteAsync();
    int Complete();

    #region Transactions
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    #endregion
}
