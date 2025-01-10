using App.Business.Model;

namespace App.Business.Services
{
    public interface IFileServicer
    {
        Task<string> AddFileAsync(FileModel model);
        Task<bool> DeleteFileAsync(string id);
        Task<FileModel> GetFileAsync(string id);
        Task<FileModel> GetFileByMd5Async(string md5);
        Task<bool> IsMultipleOwnerAsync(string id, string ownerId);
    }
}