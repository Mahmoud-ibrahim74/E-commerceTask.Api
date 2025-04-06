using E_commerceTask.Application.DTOs.Requests.Orders;
using E_commerceTask.Application.DTOs.Response;

namespace E_commerceTask.Application.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<Response<OrderDTO>> GetByIdAsync(int id);
        Task<Response<string>> CreateAsync(CreateOrderDto dto);
        Task<Response<string>> UpdateStatusAsync(int id);

    }
}
