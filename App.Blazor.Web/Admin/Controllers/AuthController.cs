using App.Business.Model;
using App.DbAccess.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Admin.Controllers
{

    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    [Area("admin")]
    [Authorize]
    [Route("api/[area]/[controller]")]
    public class AuthController : ControllerBase
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
                return Unauthorized("用户信息不存在，请检查您输入的账户。");
            }
            var signIn = await _signInManager.PasswordSignInAsync(user, model.Pwd, false, false);
            if (signIn.Succeeded)
            {
                return true;
            }
            else if (signIn.IsLockedOut)
            {
                return Unauthorized("用户已经被锁定，请联系管理员解锁。");
            }
            else
            {
                return Unauthorized("用户名或密码错误。");
            }
        }

        [HttpPost("signout")]
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        [HttpGet("claims")]
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetUserClaims()
        {
            var result = HttpContext.User.Claims.ToDictionary(s => s.Type, s => s.Value);
            return await Task.FromResult(result);
        }
    }
}
