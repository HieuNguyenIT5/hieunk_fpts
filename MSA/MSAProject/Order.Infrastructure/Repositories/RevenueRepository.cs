using Order.Domain.AggregateModels;

namespace Order.Infrastructure.Repositories;

public class RevenueRepository : IRevenueRepository
{
    private readonly DbContextModel _dbContext;
    public RevenueRepository(DbContextModel dbContext)
    {
        this._dbContext = dbContext;
    }
    public void Add(int orderId, decimal total)
    {
        Revenue revenue = new Revenue(orderId, total);
        _dbContext.Add(revenue);
        _dbContext.SaveChanges();
    }
}
