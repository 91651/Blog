using Lazy.SlideCaptcha.Core;
using Lazy.SlideCaptcha.Core.Validator;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptcha _captcha;

        public CaptchaController(ICaptcha captcha)
        {
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
            return _captcha.Validate(id, track);
        }
    }
}
