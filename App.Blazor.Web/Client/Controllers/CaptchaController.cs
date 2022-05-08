using System.Text.Json;
using App.Captcha;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.Blazor.Web.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaptchaController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        public CaptchaController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public Task<CaptchaModel> GenerateCaptcha()
        {
            var captcha = new ImageCaptcha().Generate();
            var point = JsonSerializer.Deserialize<Point>(JsonSerializer.Serialize(captcha.Point));
            _memoryCache.Set(nameof(ImageCaptcha), point, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) });
            captcha.Point.X = default;
            captcha.Point.Y = default;
            return Task.FromResult(captcha);
        }

        [HttpPost]
        public Task<string> VerifyCaptcha(Point point)
        {
            var p = _memoryCache.Get<Point>(nameof(ImageCaptcha));
            _memoryCache.Remove(nameof(ImageCaptcha));
            var code = ImageCaptcha.CaptchaVerify(p, point);
            if (!string.IsNullOrWhiteSpace(code))
            {
                _memoryCache.Set(code, true);
                return Task.FromResult(code);
            }
            return Task.FromResult(string.Empty);
        }
    }
}
