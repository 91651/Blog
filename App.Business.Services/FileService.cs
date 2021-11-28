using App.Business.Services.Models;
using App.DbAccess.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using File = App.DbAccess.Entities.File;

namespace App.Business.Services
{
    public class FileService : IFileServicer
    {
        private readonly IMapper _mapper;
        private readonly IRepository<File> _fileRepository;

        public FileService(IMapper mapper, IRepository<File> fileRepository)
        {
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task<string> AddFileAsync(FileModel model)
        {
            var entity = _mapper.Map<File>(model);
            entity.Id = Guid.NewGuid().ToString();
            await _fileRepository.AddAsync(entity);
            await _fileRepository.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<bool> DelFileAsync(string id)
        {
            _fileRepository.Remove(id);
            var rows = await _fileRepository.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<FileModel> GetFileAsync(string id)
        {
            var entity = await _fileRepository.GetByIdAsync(id);
            return _mapper.Map<FileModel>(entity);
        }
        public async Task<FileModel> GetFileByMd5Async(string md5)
        {
            var entity = await _fileRepository.GetAll().FirstOrDefaultAsync(f => f.Md5 == md5);
            return _mapper.Map<FileModel>(entity);
        }
        public async Task<bool> IsMultipleOwnerAsync(string id, string ownerId)
        {
            return await _fileRepository.GetAll().Where(f => f.Id == id && f.OwnerId != ownerId).AnyAsync();
        }
    }
}
