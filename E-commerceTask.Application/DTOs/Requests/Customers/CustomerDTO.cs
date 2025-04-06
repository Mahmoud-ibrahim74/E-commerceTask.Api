namespace E_commerceTask.Application.DTOs.Requests.Customers
{
    public record CustomerDTO
    {
        public int id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string? Phone { get; init; }
    }
}
