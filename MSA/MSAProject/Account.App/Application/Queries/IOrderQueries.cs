namespace Account.App.Application.Queries
{
    public interface IOrderQueries
    {
        public List<Order> GetOrderByStatus(int status);
        public List<Order> getOrderByCustomerID(string cus_id);
    }
}
