using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Admin.Controllers
{
    [ApiController]
    [Area("admin")]
    [Authorize]
    [Route("api/[area]/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Index()
        {
            var time = DateTime.Now;
            return Ok(time);
        }
    }
}