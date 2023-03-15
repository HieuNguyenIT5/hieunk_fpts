using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class writeFileController : ControllerBase
    {
        private readonly string _outputFolder;

        public writeFileController(IConfiguration config)
        {
            _outputFolder = config.GetValue<string>("OutputFolder");
        }

        [HttpPost]
        [Route("write_file")]
        public async Task<IActionResult> WriteFile()
        {
            try
            {
                // Đọc chuỗi JSON từ luồng đầu vào của yêu cầu.
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    string jsonString = await reader.ReadToEndAsync();
                    var obj = JsonConvert.DeserializeObject(jsonString);
                    // Lưu chuỗi vào một file.
                    string filePath = Path.Combine(_outputFolder, "output.json");
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(obj);
                    }
                    // Trả về kết quả thành công cho client.
                    return Ok(new { message="Lưu chuỗi json thành công!"});
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Lưu file thất bại: " + e.Message });
            }
        }
    }
}
