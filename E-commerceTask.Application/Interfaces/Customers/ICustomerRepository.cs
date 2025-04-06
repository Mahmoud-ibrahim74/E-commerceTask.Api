using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerceTask.Domain.Models.Customers;

namespace E_commerceTask.Application.Interfaces.Customers
{
    public interface ICustomerRepository:IBaseRepository<Customer>
    {
    }
}
