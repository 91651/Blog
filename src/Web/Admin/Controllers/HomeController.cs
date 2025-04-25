using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Admin.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "admin")]
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
        var time = DateTime.Now;
        return Ok(time);
    }
}