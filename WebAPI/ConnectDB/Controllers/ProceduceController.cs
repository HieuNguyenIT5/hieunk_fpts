using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using Oracle.ManagedDataAccess.Types;

namespace ConnectDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceduceController : ControllerBase
    {
        public const string connectionString = "Data Source=localhost:1521/ORCLCDB;User Id=test;Password=Test1234;";
        
        [HttpPost]
        [Route("addBuyer")]
        public IActionResult AddBuyer([FromBody] buyer buyer)
        {
            var connection = new OracleConnection(connectionString);
            try
            {
                connection.Open();
                var command = new OracleCommand("addBuyer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Input).Value = buyer.id;
                command.Parameters.Add("name", OracleDbType.Varchar2, ParameterDirection.Input).Value = buyer.name;
                command.Parameters.Add("paymentMethod", OracleDbType.Varchar2, ParameterDirection.Input).Value = buyer.paymentmethod;
                command.ExecuteNonQuery();
                return Ok(new {message= "Thêm dữ liệu thành công!"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateBuyer")]
        public IActionResult UpdateBuyer([FromBody] buyer buyer)
        {
            var connection = new OracleConnection(connectionString);
            try
            {
                connection.Open();
                var command = new OracleCommand("updateBuyer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Input).Value = buyer.id;
                command.Parameters.Add("name", OracleDbType.Varchar2, ParameterDirection.Input).Value = buyer.name;
                command.Parameters.Add("paymentMethod", OracleDbType.Varchar2, ParameterDirection.Input).Value = buyer.paymentmethod;
                command.Parameters.Add("success", OracleDbType.Int32, ParameterDirection.Output);
                // Thực thi stored procedure
                command.ExecuteNonQuery();
                // Lấy giá trị trả về từ output parameter

                int success = ((OracleDecimal)command.Parameters["success"].Value).ToInt32();
                

                // Xử lý kết quả
                if (success == 1)
                {
                    return Created( new { message = "Cập nhật dữ liệu thành công!" });
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private IActionResult Created(object value)
        {
            throw new NotImplementedException();
        }
    }
}
