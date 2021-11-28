using App.Business.Services.Models;
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
        Task<string> AddChannelAsync(ChannelModel model);
    }
}