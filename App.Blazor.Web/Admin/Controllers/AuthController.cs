using App.Blazor.Admin.Model;
using App.DbAccess.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Admin.Controllers
{

    [ApiController]
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("signin"), AllowAnonymous]
        public async Task<ActionResult<bool>> SignIn(SignInModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            var signIn = await _signInManager.PasswordSignInAsync(user, model.Pwd, false, false);
            if (signIn.Succeeded)
            {
                return true;
            }
            else if (signIn.IsLockedOut)
            {
                return Unauthorized();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("claims")]
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetUserClaims()
        {
            var result = HttpContext.User.Claims.Select(s => new KeyValuePair<string, string>(s.Type, s.Value));
            return await Task.FromResult(result);
        }
    }
}
