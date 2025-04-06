using E_commerceTask.Domain.Enums;
using E_commerceTask.Domain.Models.Customers;
using E_commerceTask.Domain.Models.Orders;
using E_commerceTask.Domain.Models.Products;

namespace E_commerceTask.Infrastructure.Seeding
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { id = 1, name = "Laptop", description = "High-end gaming laptop", price = 1500.00, stock = 10 },
                new Product { id = 2, name = "Mouse", description = "Wireless mouse", price = 25.00, stock = 100 },
                new Product { id = 3, name = "Keyboard", description = "Mechanical keyboard", price = 70.00, stock = 50 }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { id = 1, name = "Mahmoud Ibrahim", email = "mahmoud@example.com", phone = "1234567890" },
                new Customer { id = 2, name = "Ahmed Ibrahim", email = "Ahmed@example.com", phone = "0987654321" }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { id = 1, customer_id = 1, order_date = DateTime.Now.Date, status = OrderStatus.Pending.ToString(), total_price = 1570.00 },
                new Order { id = 2, customer_id = 2, order_date = DateTime.Now.AddDays(-2).Date, status = OrderStatus.Delivered.ToString(), total_price = 95.00 }
            );

            // Seed OrderProduct (join table)
            modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct { id = 1, order_id = 1, product_id = 1 },
                new OrderProduct { id= 2, order_id = 1, product_id = 3 },
                new OrderProduct { id= 3, order_id = 2, product_id = 2 },
                new OrderProduct {id = 4, order_id = 2, product_id = 3 }
            );

        }
    }
}
