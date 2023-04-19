using Account.Domain.AggregateModels;

namespace Account.Infrastructure.Repositories;

public interface ICustomerRepository
{
    public Customer FindCustomer(string id);
    public decimal getCustomerWallet(string id);
}
