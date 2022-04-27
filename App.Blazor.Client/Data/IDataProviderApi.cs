using App.Business.Model;
using App.EFCore.DynamicLinq;
using Refit;

namespace App.Blazor.Client.Data
{
    public interface IDataProviderApi
    {
        [Get("/api/article/{id}")]
        Task<ArticleModel> GetArticleAsync(string id);
        [Get("/api/prev")]
        Task<ArticleModel> GetPrevArticleAsync([Query]string chennelId, [Query]string id);
        [Get("/api/{id}/next")]
        Task<ArticleModel> GetNextArticleAsync([Query]string chennelId, [Query]string id);
        [Put("/api/article/{id}/viewed")]
        Task UpdateArticleViewedAsync(string id);
        [Get("/api/channel/{id}")]
        Task<PageResult<List<ArticleListModel>>> GetArticlesAsync([Query] ArticleQueryModel query);

        [Get("/api/channel/{id}")]
        Task<ChannelModel> GetChannelAsync(string id);

        [Post("/api/comment")]
        Task<bool> AddCommentAsync(CommentModel model, string captchaCode);
        [Get("/api/comment/{ownerId}")]
        Task<List<CommentModel>> GetCommentsAsync(string ownerId);

    }
}
