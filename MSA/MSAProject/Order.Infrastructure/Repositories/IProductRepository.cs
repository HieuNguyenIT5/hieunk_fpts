using Order.Domain.AggregateModels;

namespace Order.Infrastructure.Repositories;

public interface IProductRepository
{
    public Product FindProduct(int idProduct);
    public void minusQuantity(int id, int number);
    public void plusQuantitySold(int id, int number);
}
