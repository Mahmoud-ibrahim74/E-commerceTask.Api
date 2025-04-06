

using E_commerceTask.Domain.Models.Customers;
using E_commerceTask.Domain.Models.Orders;
using E_commerceTask.Domain.Models.Products;
using E_commerceTask.Infrastructure.Seeding;

namespace E_commerceTask.Infrastructure.Data
{
    public class ECommerceTaskContext(DbContextOptions<ECommerceTaskContext> options) : DbContext(options)
    {


        #region DbSets
        public DbSet<Order> Orders { get; set; }    
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderProduct> ProductsProducts { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();


        }
    }
}
