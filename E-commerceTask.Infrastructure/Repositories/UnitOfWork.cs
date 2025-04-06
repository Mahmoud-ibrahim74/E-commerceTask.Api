using E_commerceTask.Application.Interfaces;
using E_commerceTask.Application.Interfaces.Customers;
using E_commerceTask.Application.Interfaces.Orders;
using E_commerceTask.Infrastructure.Data;
using E_commerceTask.Infrastructure.Repositories.Orders;
using Microsoft.Extensions.Configuration;

namespace E_commerceTask.Infrastructure.Repositories;

public class UnitOfWork(ECommerceTaskContext context, IConfiguration config) : IUnitOfWork
{
    private readonly ECommerceTaskContext _context = context;
    protected readonly IConfiguration _config = config;

    public IOrderRepository Order { get; private set; } = new OrderRepository(context);

    public IProductRepository Product { get; private set; } = new ProductRepository(context);

    public ICustomerRepository Customer { get; private set; } = new CustomerRepository(context);

    public IOrderProductRepository OrderProduct { get; private set; } = new OrderProductRepository(context);

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
    public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
    public async Task CommitTransactionAsync() => await _context.Database.CommitTransactionAsync();
    public async Task RollbackTransactionAsync() => await _context.Database.RollbackTransactionAsync();
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }

    public int Complete() => _context.SaveChanges();
}
