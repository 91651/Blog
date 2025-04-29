using Blog.Model;
using Blog.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Admin.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "admin")]
[Area("admin")]
[Authorize]
[Route("api/[area]/[controller]")]
public class ChannelController : ControllerBase
{
    private readonly IChannelService _channelService;

    public ChannelController(IChannelService channelService)
    {
        _channelService = channelService;
    }

    [HttpPost]
    public Task<string> AddChannel(ChannelModel model)
    {
        return _channelService.AddChannelAsync(model);
    }

    [HttpDelete("{id}")]
    public Task<bool> DeleteChannel(string id)
    {
        return _channelService.DeleteChannelAsync(id);
    }

    [HttpPut("{id}")]
    public Task<bool> UpdateChannel(ChannelModel model)
    {
        return _channelService.UpdateChannelAsync(model);
    }

    [HttpGet]
    public Task<List<ChannelModel>> GetChannels()
    {
        return _channelService.GetChannelsAsync();
    }
}