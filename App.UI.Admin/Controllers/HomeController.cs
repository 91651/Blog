using Microsoft.AspNetCore.Mvc;

namespace App.UI.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var time = DateTime.Now;
            return Ok(time);
        }
    }
}