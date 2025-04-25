﻿using Blog.Model;

namespace Blog.Service;

public interface IChannelService
{
    Task<string> AddChannelAsync(ChannelModel model);

    Task<bool> DeleteChannelAsync(string id);

    Task<bool> UpdateChannelAsync(ChannelModel model);

    Task<List<ChannelModel>> GetChannelsAsync();

    Task<ChannelModel> GetChannelAsync(string id);

    Task<List<ChannelModel>> GetChannelsAsync(string pid = null);
}