using Order.Domain.AggregateModels;

namespace Order.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbContextModel _dbContext;
    public ProductRepository(DbContextModel dbContext) 
    {
        this._dbContext = dbContext;
    }
    public Product FindProduct(int idProduct)
    {
        return _dbContext.Products.Find(idProduct);
    }
    public void minusQuantity(int id, int number)
    {
        Product product = _dbContext.Products.Find(id);
        product.minusQuantity(number);
        _dbContext.SaveChanges();
    }
    public void plusQuantitySold(int id, int number)
    {
        Product product = _dbContext.Products.Find(id);
        product.plusQuantitySold(number);
        _dbContext.SaveChanges();
    }
}
