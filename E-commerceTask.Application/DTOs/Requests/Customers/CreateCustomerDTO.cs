using System.ComponentModel.DataAnnotations;
namespace E_commerceTask.Application.DTOs.Requests.Customers
{
    public class CreateCustomerDTO
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
