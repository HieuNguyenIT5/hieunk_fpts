using Order.Domain.AggregateModels;

namespace Order.Infrastructure.Repositories;

public interface ICustomerRepository
{
    public Customer FindCustomer(string id);
    public void minusCustomerWallet(string id, decimal number);
}
