using E_commerceTask.Application.Interfaces.Orders;
using E_commerceTask.Domain.Models.Orders;
using E_commerceTask.Infrastructure.Data;

namespace E_commerceTask.Infrastructure.Repositories.Orders
{
    public class OrderProductRepository(ECommerceTaskContext context) : BaseRepository<OrderProduct>(context), IOrderProductRepository
    {
    }
}
