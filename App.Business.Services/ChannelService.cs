using System.Security.Cryptography;
using App.Business.Model;
using App.DbAccess.Entities;
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
            await _channelRepository.AddAsync(entity);
            await _channelRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DelChannelAsync(string id)
        {
            var entity = await _channelRepository.GetByIdAsync(id);
            entity.State = 0;
            var rows = await _channelRepository.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> UpdateChannelAsync(ChannelModel model)
        {
            var entity = _mapper.Map<Channel>(model);
            _channelRepository.Update(entity);
            var rows = await _channelRepository.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<List<ChannelModel>> GetChannelsAsync()
        {
            var channels = await _channelRepository.GetAll().Where(c => c.State != 0).ToListAsync();
            return _mapper.Map<List<ChannelModel>>(channels);
        }

        public async Task<ChannelModel> GetChannelAsync(string id)
        {
            var entity = await _channelRepository.GetByIdAsync(id);
            return _mapper.Map<ChannelModel>(entity);
        }
        public async Task<List<ChannelModel>> GetChannelsAsync(string pid = null)
        {
            var channels = await _channelRepository.GetAll().Where(c => c.State == 1 && c.ParentId == pid).ToListAsync();
            var list = _mapper.Map<List<ChannelModel>>(channels);
            return list;
        }

    }
}
