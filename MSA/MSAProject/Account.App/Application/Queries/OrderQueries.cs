namespace Account.App.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;
        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Orders> GetOrderByStatus(int status)
        {
            return _orderRepository.GetOrderByStatus(status);
        }

        public List<Orders> getOrderByCustomerID(string cus_id)
        {
            return _orderRepository.getOrderByCustomerId(cus_id);
        }
    }
}
