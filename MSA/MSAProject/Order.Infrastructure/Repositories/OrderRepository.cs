using Order.Domain.AggregateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextModel _dbContextModel;
        public OrderRepository(DbContextModel dbContextModel) 
        {
            this._dbContextModel = dbContextModel;   
        }
        public Orders addOder(Orders order)
        {
            _dbContextModel.Orders.Add(order);
            _dbContextModel.SaveChanges();
            return order;
        }
    }
}
