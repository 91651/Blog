using Lazy.SlideCaptcha.Core;
using Lazy.SlideCaptcha.Core.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.Blazor.Web.Client.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "client")]
    [Route("api/[controller]")]
    public class CaptchaController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ICaptcha _captcha;

        public CaptchaController(IMemoryCache memoryCache, ICaptcha captcha)
        {
            _memoryCache = memoryCache;
            _captcha = captcha;
        }

        [HttpGet]
        public CaptchaData Generate()
        {
            return _captcha.Generate();
        }

        [HttpPost]
        public ValidateResult Validate([FromQuery] string id, SlideTrack track)
        {
            var validateResult = _captcha.Validate(id, track);
            if (validateResult.Result == ValidateResult.ValidateResultType.Success)
            {
                _memoryCache.Set($"captcha-{id}", true, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });
            }
            return validateResult;
        }
    }
}
