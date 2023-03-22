using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IMemoryCache _cache;
    public BasketController(IMemoryCache cache)
    {
        _cache = cache;
    }

    [HttpPost("/addtocart")]
    public IActionResult AddToCart(Basket basket)
    {
        if (basket == null)
        {
            return BadRequest(
                new { 
                    message = "Dữ liệu đầu vào không hợp lệ" 
                }
            );
        }

        if (basket.Units <= 0 || basket.UnitPrice <= 0)
        {
            return BadRequest(
                new { 
                    message = "Số lượng sản phẩm và giá sản phẩm phải lớn hơn 0" 
                }
            );
        }

        string cartKey = $"cart-{basket.BuyerId}";
        // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng hay chưa
        if (_cache.TryGetValue(cartKey, out List<Basket> cart))
        {
            var existingProduct = 
                cart.FirstOrDefault(
                    p => p.ProductId == basket.ProductId
                );
            if (existingProduct != null)
            {
                // Nếu sản phẩm đã tồn tại trong giỏ hàng, tăng số lượng lên
                existingProduct.Units += basket.Units;
            }
            else
            {
                // Nếu sản phẩm chưa tồn tại trong giỏ hàng, thêm mới vào
                cart.Add(basket);
            }
        }
        else
        {
            // Nếu giỏ hàng chưa được tạo, tạo mới và thêm sản phẩm vào
            cart = new List<Basket> { basket};
            //Tạo ra một cache để nhớ key của từng giỏ hàng
            if (_cache.TryGetValue("listkey", out List<string> listKey))
            {
                listKey.Add(cartKey);
            }
            else
            {
                listKey = new List<string> { cartKey };
            }
            _cache.Set("listkey", listKey);
        }

        // Lưu giỏ hàng vào cache với khóa cartKey
        _cache.Set(cartKey, cart);
        return Ok(new
        {
            message = "Đã thêm một sản phẩm vào giỏ hàng"
        });
    }

    [HttpGet("/getbasket")]
    public IActionResult GetCart(string buyerId)
    {
        string cartKey = $"cart-{buyerId}";
        if (_cache.TryGetValue(cartKey, out List<Basket> cart))
        {
            return Ok(cart);
        }
        else
        {
            return Ok(new List<Basket>());
        }
    }

    [HttpGet("/getallcart")]
    public IActionResult GetAllCart()
    {
        if (_cache.TryGetValue("listkey", out List<string> keys))
        {
            var cartItems = new List<Basket>();
            foreach (var cacheKey in keys)
            {
                if (cacheKey.ToString().StartsWith("cart-"))
                {
                    var cart = _cache.Get<List<Basket>>(cacheKey.ToString());
                    cartItems.AddRange(cart);
                }
            }
            return Ok(cartItems);
        }
        return Ok(new List<Basket>());
    }
}
