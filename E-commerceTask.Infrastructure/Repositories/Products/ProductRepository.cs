using E_commerceTask.Application.Interfaces.Orders;
using E_commerceTask.Domain.Models.Products;
using E_commerceTask.Infrastructure.Data;

namespace E_commerceTask.Infrastructure.Repositories.Orders
{
    public class ProductRepository(ECommerceTaskContext context) : BaseRepository<Product>(context), IProductRepository
    {
    }
}
