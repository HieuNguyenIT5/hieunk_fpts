using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BuyerController : ControllerBase
{
    private readonly OraDbContext _dbContext;

    public BuyerController(OraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("/listbuyer")]
    public IActionResult getListBuyer()
    {
        try
        {
            var listBuyer = _dbContext.Buyer.ToList();
            return Ok(listBuyer);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("/addbuyer")]
    public IActionResult addBuyer(Buyer buyer)
    {
        try
        {
            _dbContext.Buyer.Add(buyer);
            _dbContext.SaveChanges();
            return Ok(new {message = "Thêm người mua thành công!"});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("/updatebuyer")]
    public IActionResult updateBuyer(Buyer buyer)
    {
        try
        {
            var buyerOld = _dbContext.Buyer.Find(buyer.id);
            if (buyerOld == null)
            {
                return NotFound(new { message = "Không tìm thấy người mua với Id tương ứng!" });
            }
            buyerOld.Name           = buyer.Name;
            buyerOld.PaymentMethod  = buyer.PaymentMethod;
            _dbContext.SaveChanges();
            return Ok(new { message = "Cập nhật người mua thành công!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("/deletebuyer")]
    public IActionResult deleteBuyer(int id)
    {
        try
        {
            var buyer = _dbContext.Buyer.Find(id);
            if(buyer == null)
            {
                return NotFound(new {message = "Người dùng không tồn tại, không thể xóa!"});
            }
            _dbContext.Buyer.Remove(buyer);
            _dbContext.SaveChanges();
            return Ok(new { message = "Xóa người dùng thành công!" });
        }catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}
