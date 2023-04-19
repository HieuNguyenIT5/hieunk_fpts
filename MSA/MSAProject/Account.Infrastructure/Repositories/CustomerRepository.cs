using Account.Domain.AggregateModels;

namespace Account.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbContextModel _dbContext;
        public CustomerRepository(DbContextModel dbContext)
        {
            this._dbContext = dbContext;
        }
        public Customer FindCustomer(string id)
        {
            return _dbContext.Customers.Find(id);
        }
        public decimal getCustomerWallet(string id)
        {
            var customer = _dbContext.Customers.Find(id);
            if(customer != null)
            {
                return customer.getCustomerWallet();
            }
            else
            {
                return -1;
            }
        }
    }
}
