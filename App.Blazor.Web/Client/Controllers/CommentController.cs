using App.Business.Model;
using App.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Client.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IChannelService _channelService;

        public CommentController(IChannelService articleService)
        {
            _channelService = articleService;
        }
    }
}
