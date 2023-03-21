using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckoutController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private readonly OraDbContext _db;
    public CheckoutController(IMemoryCache cache, OraDbContext db)
    {
        _db = db;
        _cache = cache;
    }

    [HttpPost("/checkout")]
    public IActionResult checkout([FromBody] Order order)
    {
        var basketKey = $"cart-{order.BuyerId}";
        if (_cache.TryGetValue(basketKey, out List<Basket> basket))
        {
            try
            {
                _db.Order.Add(order);
                _db.SaveChanges();
                foreach(var item in basket)
                {
                    var orderItem = new OrderItem();
                    orderItem.OrderId = order.id;
                    orderItem.ProductId = item.ProductId;
                    orderItem.Units = item.Units;
                    orderItem.UnitPrice = item.UnitPrice;
                    _db.OrderItem.Add(orderItem);
                    _db.SaveChanges();
                }
                return Ok(new {message = "Bạn đã đặt hàng thành công!"});
            }catch(Exception ex)
            {
                return BadRequest(new { message = "Bạn đã đặt hàng thất bại!", error = ex.Message });
            }

        }
        else
        {
            return NotFound(new { message = "Bạn không có gì trong giỏ hàng!"});
        }
    }
}
