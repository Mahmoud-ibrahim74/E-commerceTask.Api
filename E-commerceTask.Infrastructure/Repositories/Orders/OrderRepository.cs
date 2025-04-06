using E_commerceTask.Application.Interfaces.Orders;
using E_commerceTask.Domain.Models.Orders;
using E_commerceTask.Infrastructure.Data;

namespace E_commerceTask.Infrastructure.Repositories.Orders
{
    public class OrderRepository(ECommerceTaskContext context) : BaseRepository<Order>(context), IOrderRepository
    {
    }
}
