using App.Business.Services.Models;

namespace App.Business.Services
{
    public interface ICommentService
    {
        Task<bool> AddCommentAsync(CommentModel model, string captchaCode);
        Task<List<CommentModel>> GetCommentsAsync(string ownerId, string pid);
    }
}