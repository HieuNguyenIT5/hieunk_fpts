namespace Account.App.Application.Queries
{
    public interface IOrderQueries
    {
        public List<Orders> GetOrderByStatus(int status);
        public List<Orders> getOrderByCustomerID(string cus_id);
    }
}
