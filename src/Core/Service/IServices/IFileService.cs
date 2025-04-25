using Blog.Model;

namespace Blog.Service;

public interface IFileServicer
{
    Task<string> AddFileAsync(FileModel model);

    Task<bool> DeleteFileAsync(string id);

    Task<FileModel> GetFileAsync(string id);

    Task<FileModel> GetFileByMd5Async(string md5);

    Task<bool> IsMultipleOwnerAsync(string id, string ownerId);
}