using E_commerceTask.Application.Interfaces.Customers;
using E_commerceTask.Domain.Models.Customers;
using E_commerceTask.Infrastructure.Data;

namespace E_commerceTask.Infrastructure.Repositories.Orders
{
    public class CustomerRepository(ECommerceTaskContext context) : BaseRepository<Customer>(context), ICustomerRepository
    {
    }
}
