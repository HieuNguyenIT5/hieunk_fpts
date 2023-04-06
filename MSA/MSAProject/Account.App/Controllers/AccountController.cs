using Account.Domain.AggregateModels;
using Account.Domain.Events;
using Account.Infrastructure;
using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Account.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IProducer<string, string> _producer;
    private readonly IMediator _mediator;
    private readonly DbContextModel _dbContextModel;
    public AccountController(IProducer<string,string> producer, DbContextModel dbContextModel, IMediator mediator)
    {
        _producer = producer;
        _mediator = mediator;
        _dbContextModel = dbContextModel;
    }

    [HttpPost("Order")]
    public async Task<IActionResult> Order(List<Order> orders)
    {
        var orderTotal = await _mediator.Send(new OrderTotalDomainEvent(orders));
        var message = await _mediator.Send(new CheckCashCustomerDomainEvent(orderTotal, orders));
        _producer.Produce("order",new Message<string, string> {Key = orders[0].CustomerId, Value = message});
        return Ok(new {success = true});
    }
}
