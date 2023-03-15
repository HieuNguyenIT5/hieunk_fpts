using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Text;
using WebAPI;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class buyerController : ControllerBase
    {
        private readonly string _connectionString;
        private IProceduceModel _proceduce;
        public buyerController(IConfiguration config)
        {
            _connectionString = config.GetValue<string>("ConnectionString");
            _proceduce = new BuyerProceduce();

        }
   
        [HttpPost]
        [Route("addBuyer")]
        public IActionResult AddBuyer([FromBody] Buyer buyer)
        {
            var connection = new OracleConnection(_connectionString);
            if (_proceduce.Create(connection, buyer))
            {
                return Ok(new { message = "Thêm dữ liệu thành công!" });
            }
            else
            {
                return BadRequest(new { message = "Thêm dữ liệu thất bại!" });
            }
        }

        [HttpPut]
        [Route("updateBuyer")]
        public IActionResult UpdateBuyer([FromBody] Buyer buyer)
        {
            var connection = new OracleConnection(_connectionString);
            try
            {
                var success = _proceduce.Update(connection, buyer);
                // Xử lý kết quả
                if (success)
                {
                    return CreatedAtAction(nameof(UpdateBuyer), new { id = buyer.id }, new { message = "Cập nhật dữ liệu thành công!" });
                }
                else
                {
                    return NotFound(new { message = "Cập nhật dữ liệu thất bại!" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteBuyer")]
        public IActionResult DeleteBuyer(int id_del)
        {
            var connection = new OracleConnection(_connectionString);
            try
            {
                var success = _proceduce.Delete(connection, id_del);
                if (success)
                {
                    return Ok(new { message = "Xóa dữ liệu thành công!" });
                }
                else
                {
                    return Ok(new { message = "Xóa dữ liệu thất bại!" });
                }
            } catch (Exception ex)
            {
                connection.Close();
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getBuyer")]
        public IActionResult GetBuyer()
        {
            var connection = new OracleConnection(_connectionString);
            try
            {
                return Ok(_proceduce.Get(connection));
            }
            catch (Exception ex)
            {
                connection.Close();
                return NotFound(ex.Message);
            }
        }
    }
}