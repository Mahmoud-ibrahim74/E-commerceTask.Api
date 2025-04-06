using E_commerceTask.Application.DTOs.Requests.Orders;
using E_commerceTask.Application.DTOs.Response;
using E_commerceTask.Application.Interfaces;
using E_commerceTask.Application.Interfaces.IServices;
using E_commerceTask.Domain.Enums;
using E_commerceTask.Domain.Models.Orders;
using E_commerceTask.Domain.Models.Products;

namespace E_commerceTask.Application.Services.Orders
{
    public class OrderService(IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<Response<string>> CreateAsync(CreateOrderDto dto)
        {
            try
            {
                var customer = await unitOfWork.Customer.GetByIdAsync(dto.CustomerId);
                if (customer == null)
                    return new()
                    {
                        Check = false,
                        Msg = "Customer not found",
                    };

                var products = await unitOfWork.Product.MasterQueryFilterAsync<Product>(p => dto.ProductIds.Contains(p.id));

                if (!products.Any())
                    return new()
                    {
                        Check = false,
                        Msg = "No valid products found"
                    };

                var total = products.Sum(p => p.price);

                var order = new Order
                {
                    customer_id = dto.CustomerId,
                    order_date = DateTime.Now,
                    status = OrderStatus.Pending.ToString(),
                    total_price = total
                };

                await unitOfWork.Order.AddAsync(order);
                var res = await unitOfWork.CompleteAsync(); // get OrderId
                if (res > 0)
                {
                    var OrderProducts = products.Select(p => new OrderProduct
                    {
                        order_id = order.id,
                        product_id = p.id
                    }).ToList();
                    await unitOfWork.OrderProduct.AddRangeAsync(OrderProducts);
                    await unitOfWork.CompleteAsync();
                    return new()
                    {
                        Check = true,
                        Msg = "Order created successfully",
                    };
                }
                return new()
                {
                    Check = true,
                    Msg = "Order created successfully",
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Check = false,
                    Msg = "Order created successfully",
                    Error = ex.Message
                };
            }


        }

        public async Task<Response<OrderDTO>> GetByIdAsync(int id)
        {
            var order = await unitOfWork.Order.GetByIdAsync(id);
            if (order is null)
                return new()
                {
                    Check = false,
                    Msg = "Order not found",
                };

            var orderdto = new OrderDTO
            {
                Id = order.id,
                CustomerName = order.customer.name,
                Status = order.status,
                NumberOfProducts = order.OrderProducts.Count
            };
            return new()
            {
                Check = true,
                Msg = "Order found",
                Data = orderdto
            };
        }

        public async Task<Response<string>> UpdateStatusAsync(int id)
        {
            try
            {
                var order = await unitOfWork.Order.GetByIdAsync(id);
                if (order is null)
                    return new()
                    {
                        Check = false,
                        Msg = "Order not found",
                    };
                if (order.status == OrderStatus.Delivered.ToString())
                    return new()
                    {
                        Check = false,
                        Msg = "Order already delivered",
                    };
                order.status = OrderStatus.Delivered.ToString();
                foreach (var orderProduct in order.OrderProducts)
                {
                    orderProduct.Product.stock -= 1;
                }
                await unitOfWork.CompleteAsync();
                return new()
                {
                    Check = true,
                    Msg = "Order status updated successfully",
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Check = false,
                    Msg = "An error occurred while updating the order status",
                    Error = ex.Message
                };

            }

        }
    }
}
