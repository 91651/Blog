using Blog.Model;

namespace Blog.Service;

public interface ICommentService
{
    Task<bool> AddCommentAsync(CommentModel model);

    Task<List<CommentModel>> GetCommentsAsync(string ownerId, string pid);
}