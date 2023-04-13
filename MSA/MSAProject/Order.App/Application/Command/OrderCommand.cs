using MediatR;
using Order.Domain.AggregateModels;
using System.Collections.Generic;

namespace Order.App.Application.Command;
public class OrderCommand : IRequest
{
    public List<OrderItem> Data{ get; set; }
    public OrderCommand(List<OrderItem> data)
    {
        this.Data = data;
    }
}
