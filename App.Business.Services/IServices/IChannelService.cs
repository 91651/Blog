using App.Business.Model;

namespace App.Business.Services
{
    public interface IChannelService
    {
        Task<string> AddChannelAsync(ChannelModel model);
        Task<List<ChannelModel>> GetChannelsAsync();
        Task<ChannelModel> GetChannelAsync(string id);
        Task<List<ChannelModel>> GetChannelsAsync(string pid = null);

    }
}