using Blog.Model;
using EFCore.DynamicLinq;
using Refit;

namespace Blog.Client.Data;

public interface IClientApiProvider
{
    #region Article Api

    [Get("/api/article/{id}")]
    Task<ArticleModel> GetArticleAsync(string id);

    [Get("/api/article/{id}/prev")]
    Task<ArticleModel> GetPrevArticleAsync(string id);

    [Get("/api/article/{id}/next")]
    Task<ArticleModel> GetNextArticleAsync(string id);

    [Put("/api/article/{id}/viewed")]
    Task UpdateArticleViewedAsync(string id);

    [Get("/api/article")]
    Task<PageResult<List<ArticleListModel>>> GetArticlesAsync([Query] ArticleQueryModel query);

    #endregion Article Api

    #region Channel Api

    [Get("/api/channel/{id}")]
    Task<ChannelModel> GetChannelAsync(string id);

    [Get("/api/channel")]
    Task<List<ChannelModel>> GetChannelsAsync();

    #endregion Channel Api

    #region Comment Api

    [Post("/api/comment")]
    Task<bool> AddCommentAsync(CommentModel model, string captchaCode);

    [Get("/api/comment/owner/{ownerId}")]
    Task<List<CommentModel>> GetCommentsAsync(string ownerId, string? pid);

    #endregion Comment Api

    #region Captcha Api

    [Get("/api/Captcha")]
    Task<T> GenerateCaptchaAsync<T>();

    [Post("/api/captcha")]
    Task<TResponse> VerifyCaptchaAsync<TRequst, TResponse>([Query] string id, TRequst track);

    #endregion Captcha Api
}