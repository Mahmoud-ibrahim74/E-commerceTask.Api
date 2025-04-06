using E_commerceTask.Application.DTOs.Requests.Customers;
using E_commerceTask.Application.DTOs.Response;
using E_commerceTask.Application.Interfaces;
using E_commerceTask.Application.Interfaces.IServices;
using E_commerceTask.Domain.Models.Customers;

namespace E_commerceTask.Application.Services.Customers
{
    public class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
    {
        public async Task<Response<string>> CreateAsync(CreateCustomerDTO dto)
        {
            try
            {
                var customer = new Customer
                {
                    name = dto.Name,
                    email = dto.Email,
                    phone = dto.Phone
                };
                await unitOfWork.Customer.AddAsync(customer);
                await unitOfWork.CompleteAsync();
                return new()
                {
                    Check = true,
                    Msg = "Customer created successfully",
                    Data = customer.id.ToString()
                };
            }
            catch (Exception ex)
            {

                return new()
                {
                    Check = false,
                    Msg = "An error occurred while creating the customer",
                    Error = ex.Message  
                };
            }
            finally
            {
                unitOfWork.Dispose();

            }

        }

        public async Task<Response<List<CustomerDTO>>> GetAllAsync()
        {
            var res = await unitOfWork.Customer.MasterQueryFilterAsync<CustomerDTO>(
                orderBySelector:x=>x.id,
                orderByTypes:Domain.Enums.OrderByTypesEnums.Descending,
                selector: x => new CustomerDTO
                {
                    id = x.id,
                    Name = x.name,
                    Email = x.email,
                    Phone = x.phone
                });
            return new()
            {
                Check = true,
                Data = res
            };  
        }

        public async Task<Response<CustomerDTO>> GetByIdAsync(int id)
        {
            var customer = await unitOfWork.Customer.GetByIdAsync(id);
            if (customer is  null)
            {
                return new()
                {
                    Check = false,
                    Msg = "Customer not found"
                };
            }
            var customerDTO = new CustomerDTO
            {
                id = customer.id,
                Name = customer.name,
                Email = customer.email,
                Phone = customer.phone
            };
            return new()
            {
                Check = true,
                Msg = "Customer found",
                Data = customerDTO
            };
        }
    }
}
