using App.Business.Model;
using App.DbAccess.Entities;
using App.DbAccess.Entities.Identity;
using App.DbAccess.Repositories;
using App.Util;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Business.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Channel> _channelRepository;

        public ChannelService(IMapper mapper, IRepository<Channel> channelRepository)
        {
            _mapper = mapper;
            _channelRepository = channelRepository;
        }

        public async Task<string> AddChannelAsync(ChannelModel model)
        {
            var channel = _channelRepository.GetAll().Where(c => c.Title == model.Title.Trim() && c.ParentId == model.ParentId).FirstOrDefault();
            if (channel != null)
            {
                return channel.Id;
            }
            var entity = _mapper.Map<Channel>(model);
            entity.Id = Guid.NewGuid().ToString(10);
            entity.State = 1;
            await _channelRepository.AddAsync(entity);
            await _channelRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<List<ChannelModel>> GetChannels()
        {
            var channels = await _channelRepository.GetAll().ToListAsync();
            return _mapper.Map<List<ChannelModel>>(channels);
        }

    }
}
