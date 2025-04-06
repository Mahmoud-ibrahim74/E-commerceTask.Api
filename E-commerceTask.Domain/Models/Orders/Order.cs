using E_commerceTask.Domain.Models.Customers;

namespace E_commerceTask.Domain.Models.Orders
{
    [Table("order")]
    public class Order
    {
        public int id { get; set; }
        public int customer_id { get; set; }
        [AllowedValues("Pending", "Delivered")]
        public string status { get; set; }  

        public DateTime order_date { get; set; }
        public double total_price { get; set; }

        [ForeignKey(nameof(customer_id))]
        public virtual Customer customer { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = [];
    }
}
