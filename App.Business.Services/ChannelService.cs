using App.Business.Model;
using App.DbAccess.Entities;
using App.DbAccess.Infrastructure;
using App.Util;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Business.Services
{
    public class ChannelService : IChannelService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ChannelService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<string> AddChannelAsync(ChannelModel model)
        {
            var channel = _db.Channels.Where(c => c.Title == model.Title.Trim() && c.ParentId == model.ParentId).FirstOrDefault();
            if (channel != null)
            {
                return channel.Id;
            }
            var entity = _mapper.Map<Channel>(model);
            entity.Id = Guid.NewGuid().ToString(10);
            await _db.Channels.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteChannelAsync(string id)
        {
            var entity = await _db.Channels.FindAsync(id);
            entity.State = 0;
            var rows = await _db.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> UpdateChannelAsync(ChannelModel model)
        {
            var entity = _mapper.Map<Channel>(model);
            _db.Channels.Update(entity);
            var rows = await _db.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<List<ChannelModel>> GetChannelsAsync()
        {
            var channels = await _db.Channels.Where(c => c.State != 0).ToListAsync();
            return _mapper.Map<List<ChannelModel>>(channels);
        }

        public async Task<ChannelModel> GetChannelAsync(string id)
        {
            var entity = await _db.Channels.FindAsync(id);
            return _mapper.Map<ChannelModel>(entity);
        }
        public async Task<List<ChannelModel>> GetChannelsAsync(string pid = null)
        {
            var channels = await _db.Channels.Where(c => c.State == 1 && c.ParentId == pid).ToListAsync();
            var list = _mapper.Map<List<ChannelModel>>(channels);
            return list;
        }

    }
}
