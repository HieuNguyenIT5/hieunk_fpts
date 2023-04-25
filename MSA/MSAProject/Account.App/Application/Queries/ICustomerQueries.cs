namespace Account.App.Application.Queries
{
    public interface ICustomerQueries
    {
        public Customer findCustommer(string cus_id);

        public decimal getCustomerWallet(string cus_id);
    }
}
