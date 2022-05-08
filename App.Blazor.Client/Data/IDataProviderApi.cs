using App.Business.Model;
using App.Captcha;
using App.EFCore.DynamicLinq;
using Refit;

namespace App.Blazor.Client.Data
{
    public interface IDataProviderApi
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
        #endregion

        #region Channel Api
        [Get("/api/channel/{id}")]
        Task<ChannelModel> GetChannelAsync(string id);
        [Get("/api/channel")]
        Task<List<ChannelModel>> GetChannelsAsync();
        #endregion

        #region Comment Api
        [Post("/api/comment")]
        Task<bool> AddCommentAsync(CommentModel model, string captchaCode);
        [Get("/api/comment/owner/{ownerId}")]
        Task<List<CommentModel>> GetCommentsAsync(string ownerId, string? pid);
        #endregion

        #region Captcha Api
        [Get("/api/Captcha")]
        Task<CaptchaModel> GenerateCaptchaAsync();
        [Post("/api/captcha")]
        Task<string> VerifyCaptchaAsync(Point point);
        #endregion

    }
}
