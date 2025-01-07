using App.Business.Model;
using App.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.Blazor.Web.Client.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "client")]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ICommentService _commentService;

        public CommentController(IMemoryCache memoryCache, ICommentService articleService)
        {
            _memoryCache = memoryCache;
            _commentService = articleService;
        }

        [HttpGet("owner/{ownerId}")]
        public Task<List<CommentModel>> GetComments(string ownerId, [FromQuery] string? pid)
        {
            return _commentService.GetCommentsAsync(ownerId, pid);
        }

        [HttpPost]
        public Task<bool> AddComments(CommentModel model, string captchaCode)
        {
            var validation = _memoryCache.TryGetValue<bool>($"captcha-{captchaCode}", out var isValid);
            if (!validation || !isValid)
            {
                return Task.FromResult(false);
            }
            return _commentService.AddCommentAsync(model);
        }
    }
}
