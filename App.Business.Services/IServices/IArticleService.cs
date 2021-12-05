using App.Business.Model;
using App.EFCore.DynamicLinq;

namespace App.Business.Services
{
    public interface IArticleService
    {
        Task<string> AddArticleAsync(ArticleModel model);
        Task<bool> DelArticleAsync(string id);
        Task<ArticleModel> GetArticleAsync(string id);
        Task<bool> UpdateArticleAsync(ArticleModel model);
        Task<PageResult<List<ArticleListModel>>> GetArticlesAsync(ArticleQueryModel model);
        Task<ArticleModel> GetPrevArticleAsync(string channelId, string id);
        Task<ArticleModel> GetNextArticleAsync(string channelId, string id);
    }
}