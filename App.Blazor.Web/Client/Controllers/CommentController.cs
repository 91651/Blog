using App.Business.Model;
using App.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService articleService)
        {
            _commentService = articleService;
        }

        [HttpGet("owner/{ownerId}")]
        public Task<List<CommentModel>> GetComments(string ownerId, [FromQuery]string? pid)
        {
            return _commentService.GetCommentsAsync(ownerId, pid);
        }
    }
}
