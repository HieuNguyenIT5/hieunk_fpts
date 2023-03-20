using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;
using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OraDbContext _db;
        public OrderController(OraDbContext db) { 
            _db = db;
        }

        [HttpGet("/getallorder")]
        public IActionResult GetAllOrder() {
            try
            {
                var orders = _db.Order.ToList();
                return Ok(orders);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/getorderdetail/{orderId}")]
        public IActionResult GetOrderDetail(int orderId)
        {
            try
            {
                var order = _db.Order.FirstOrDefault(o => o.id == orderId);
                if (order == null)
                {
                    return NotFound();
                }
                _db.OrderItem.Where(o=>o.OrderId == orderId).ToList();
         
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
