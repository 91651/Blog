﻿using Blog.Model;
using Blog.Service;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Client.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "client")]
[Route("api/[controller]")]
public class ChannelController : ControllerBase
{
    private readonly IChannelService _channelService;

    public ChannelController(IChannelService channelService)
    {
        _channelService = channelService;
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