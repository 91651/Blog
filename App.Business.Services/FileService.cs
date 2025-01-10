using App.Business.Model;
using App.DbAccess.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using File = App.DbAccess.Entities.File;

namespace App.Business.Services
{
    public class FileService : IFileServicer
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public FileService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<string> AddFileAsync(FileModel model)
        {
            var entity = _mapper.Map<File>(model);
            entity.Id = Guid.NewGuid().ToString();
            await _db.Files.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<bool> DeleteFileAsync(string id)
        {
            var entity = await _db.Files.FindAsync(id);
            _db.Files.Remove(entity);
            var rows = await _db.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<FileModel> GetFileAsync(string id)
        {
            var entity = await _db.Files.FindAsync(id);
            return _mapper.Map<FileModel>(entity);
        }
        public async Task<FileModel> GetFileByMd5Async(string md5)
        {
            var entity = await _db.Files.FirstOrDefaultAsync(f => f.Md5 == md5);
            return _mapper.Map<FileModel>(entity);
        }
        public async Task<bool> IsMultipleOwnerAsync(string id, string ownerId)
        {
            return await _db.Files.Where(f => f.Id == id && f.OwnerId != ownerId).AnyAsync();
        }
    }
}
