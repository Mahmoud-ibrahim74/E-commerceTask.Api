using System.ComponentModel.DataAnnotations;

namespace E_commerceTask.Application.DTOs.Requests.Orders
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
    }
}
