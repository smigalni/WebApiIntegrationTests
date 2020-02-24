using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateCommand command,
            CancellationToken cancellationToken)
        {
            return Ok(command);
        }

        [HttpPost("ReadFileTest")]
        public IActionResult ReadFileTest(IFormFile file)
        {
            return Ok();
        }
        [HttpPost("ReadFile/{fileName}")]
        public IActionResult ReadFile(IFormFile file, string fileName, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
