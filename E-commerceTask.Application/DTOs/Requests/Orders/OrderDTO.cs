namespace E_commerceTask.Application.DTOs.Requests.Orders
{
    public record OrderDTO
    {
        public int Id { get; init; }
        public string CustomerName { get; init; }
        public string Status { get; init; }
        public int NumberOfProducts { get; init; }
    }
}
