using App.Business.Model;
using App.DbAccess.Entities;

namespace App.Business.Services
{
    public interface IChannelService
    {
        Task<string> AddChannelAsync(ChannelModel model);
        Task<List<ChannelModel>> GetChannels();

    }
}