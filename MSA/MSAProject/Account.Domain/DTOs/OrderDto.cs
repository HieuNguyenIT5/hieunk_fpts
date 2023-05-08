using Account.Domain.AggregateModels;

namespace Account.Domain.DTOs
{
    public class OrderDto
    {
        public string CustomerId { get; set; }
        public string IP { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
