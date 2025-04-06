using E_commerceTask.Application.Interfaces;
using E_commerceTask.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace E_commerceTask.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceTaskContext _context;
    protected readonly IConfiguration _config;



    public UnitOfWork(ECommerceTaskContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

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
