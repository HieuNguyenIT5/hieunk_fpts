using Account.Domain.AggregateModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Events
{
    public class CheckCashCustomerDomainEvent :IRequest<string>
    {
        public decimal orderTotal{ get; set; }
        public List<Order> orders { get; set; }

        public CheckCashCustomerDomainEvent(decimal orderTotal, List<Order> orders)
        {
            this.orderTotal = orderTotal;
            this.orders = orders;
        }
    }
}
