namespace E_commerceTask.Domain.Models.Products
{
    [Table("product")] 
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
    }
}
