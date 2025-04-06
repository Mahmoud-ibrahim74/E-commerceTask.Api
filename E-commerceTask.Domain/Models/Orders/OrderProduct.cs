using E_commerceTask.Domain.Models.Products;

namespace E_commerceTask.Domain.Models.Orders
{
    [Table("order_product")]
    public class OrderProduct
    {
        [Key]
        public int id { get; set; } 
        public int order_id {  get; set; }  
        public int product_id { get; set; }

        [ForeignKey(nameof(order_id))]
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(product_id))]
        public virtual Product Product { get; set; }        
    }
}
