namespace Account.App.Application.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private readonly ICustomerRepository _custommerRepo;
        public CustomerQueries(ICustomerRepository custommerRepo)
        {
            _custommerRepo = custommerRepo;
        }
        public Customer findCustommer(string cus_id)
        {
            return _custommerRepo.FindCustomer(cus_id);
        }

        public decimal getCustomerWallet(string cus_id)
        {
            return _custommerRepo.getCustomerWallet(cus_id);
        }
    }
}
