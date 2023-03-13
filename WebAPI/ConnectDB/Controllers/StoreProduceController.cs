using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace ConnectDB.Controllers
{
    public class StoreProduceController : Controller
    {
        [HttpGet]
        [Route("/addBuyer")]
        public IActionResult GetBuyers()
        {
            var connectionString = "Data Source=localhost:1521/ORCLCDB;User Id=test;Password=Test1234;";
            using var connection = new OracleConnection(connectionString);
            try
            {
                connection.Open();
                var command = new OracleCommand("SELECT * FROM Buyer", connection);
                var reader = command.ExecuteReader();

                var buyers = new List<object>();
                while (reader.Read())
                {
                    var buyer = new
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        PaymentMethod = reader.GetString(2)
                    };
                    buyers.Add(buyer);
                }

                return Json(buyers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
