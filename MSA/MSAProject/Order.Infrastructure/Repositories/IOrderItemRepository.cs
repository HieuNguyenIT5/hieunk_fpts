using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public interface IOrderItemRepository
    {
        public void AddOrderItem(string CustomerId, int ProductId, int Quantity, decimal Price, string IP);
        public int GetLastOrderId();
    }
}
