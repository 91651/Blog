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
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public HomeController(IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Index()
        {
            var path = Path.Combine(_hostEnvironment.WebRootPath, _configuration["AppSettings:StaticContentPath"]);
            var uploadPath = _configuration["AppSettings:ImgUploadPath"]; //避免路径敏感，使用"/"
            var fullPath = Path.GetFullPath(Path.Combine(path, uploadPath));
            var time = DateTime.Now;
            return Ok(time);
        }
    }
}