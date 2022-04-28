using App.Business.Model;
using App.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _channelService;

        public ChannelController(IChannelService articleService)
        {
            _channelService = articleService;
        }

        [HttpGet("{id}")]
        public Task<ChannelModel> GetChannel(string id)
        {
            return _channelService.GetChannelAsync(id);
        }

        [HttpGet]
        public Task<List<ChannelModel>> GetChannels()
        {
            return _channelService.GetChannelsAsync();
        }
    }
}
