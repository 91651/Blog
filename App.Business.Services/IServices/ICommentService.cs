using App.Business.Model;

namespace App.Business.Services
{
    public interface ICommentService
    {
        Task<bool> AddCommentAsync(CommentModel model, string captchaCode);
        Task<List<CommentModel>> GetCommentsAsync(string ownerId, string pid);
    }
}