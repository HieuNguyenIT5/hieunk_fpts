using Order.Domain.AggregateModels;

namespace Order.Infrastructure.Repositories
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
        public void minusCustomerWallet(string id, decimal number)
        {
            var customer = _dbContext.Customers.Find(id);
            customer.minusCustomerWallet(number);
            _dbContext.SaveChanges();   
        }
    }
}
