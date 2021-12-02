using App.Business.Model;
using App.Business.Services;
using App.EFCore.DynamicLinq;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Admin.Controllers
{
    [ApiController]
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _channelService;

        public ChannelController(IChannelService articleService)
        {
            _channelService = articleService;
        }

        [HttpPost("/api/channel")]
        public Task<string> AddChannel(ChannelModel model)
        {
            return _channelService.AddChannelAsync(model);
        }

        [HttpGet]
        public Task<List<ChannelModel>> GetChannels()
        {
            return _channelService.GetChannels();
        }
    }
}
