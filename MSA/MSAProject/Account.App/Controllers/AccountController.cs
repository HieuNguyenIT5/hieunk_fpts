using Account.Domain.AggregateModels;
using Account.Domain.Events;
using Account.Infrastructure;
using Account.Infrastructure.Repositories;
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
    private readonly ICustomerRepository _customerRepo;
    private readonly IOrderRepository _orderRepo;
    public AccountController(
        IProducer<string, string> producer,
        DbContextModel dbContextModel,
        IMediator mediator,
        ICustomerRepository customerRepo,
        IOrderRepository orderRepo
        )
    {
        //_producer = producer;
        _mediator = mediator;
        _dbContextModel = dbContextModel;
        _customerRepo = customerRepo;
        _orderRepo = orderRepo;
    }

    [HttpPost("Order")]
    public async Task<IActionResult> Order(List<Order> orders)
    {
        var orderTotal = await _mediator.Send(new OrderTotalDomainEvent(orders));
        var message = await _mediator.Send(new CheckCashCustomerDomainEvent(orderTotal, orders));
        //_producer.Produce("order",new Message<string, string> {Key = orders[0].CustomerId, Value = message});
        return Ok(new {success = true});
    }

    [HttpGet("GetCustomerWallet/{id}")]
    public IActionResult getCustomerWaller(string id)
    {
        var balance = _customerRepo.getCustomerWallet(id);
        if(balance == -1)
        {
            return BadRequest(new { message = "Khách hàng không tồn tại!" });
        }
        return Ok(new {balance = balance.ToString()});
    }

    [HttpGet("GetSuccessfulOrders")]
    public IActionResult getSuccessfulOrders()
    {
        List<Order> orders = _orderRepo.GetOrderByStatus(1);
        if (orders.Count() != 0)
        {
            return Ok(orders);
        }
        return BadRequest(new { message = "Không có đơn hàng nào!" });
    }
    [HttpGet("GetOrderFaile")]
    public IActionResult getOrderFaile()
    {
        List<Order> orders = _orderRepo.GetOrderByStatus(0);
        if  (orders.Count() != 0)
        {
            return Ok(orders);
        }
        return BadRequest(new { message = "Không có đơn hàng nào!" });
    }
}
