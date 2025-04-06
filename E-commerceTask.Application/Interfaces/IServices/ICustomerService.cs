using E_commerceTask.Application.DTOs.Requests.Customers;
using E_commerceTask.Application.DTOs.Response;

namespace E_commerceTask.Application.Interfaces.IServices
{
    public interface ICustomerService
    {
        public  Task<Response<List<CustomerDTO>>> GetAllAsync();
        Task<Response<CustomerDTO>> GetByIdAsync(int id);
        Task<Response<string>> CreateAsync(CreateCustomerDTO dto);
    }
}
