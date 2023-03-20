using EF_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly OraDbContext _dbContext;
        public BuyerController(OraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("/addbuyer")]
        public IActionResult AddBuyer(Buyer buyer)
        {
            _dbContext.Buyer.Add(buyer);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
