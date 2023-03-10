using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ReadFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class fileController : ControllerBase
    {
        private readonly string _inputFolder;

        public fileController(IConfiguration config)
        {
            _inputFolder = config.GetValue<string>("InputFolder");
        }
        [HttpGet]
        [Route("read_file")]
        public IActionResult ReadFileToObject()
        {
            using (StreamReader r = new StreamReader(_inputFolder))
            {
                string json = r.ReadToEnd();
                return Ok(json);
            }
        }
    }
}
