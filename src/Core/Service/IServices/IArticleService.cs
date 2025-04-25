using Blog.Model;
using EFCore.DynamicLinq;

namespace Blog.Service;

public interface IArticleService
{
    Task<string> AddArticleAsync(ArticleModel model);

    Task<bool> DeleteArticleAsync(string id);

    Task<ArticleModel> GetArticleAsync(string id);

    Task<bool> UpdateArticleAsync(ArticleModel model);

    Task<PageResult<List<ArticleListModel>>> GetArticlesAsync(ArticleQueryModel model);

    Task<ArticleModel> GetPrevArticleAsync(string id);

    Task<ArticleModel> GetNextArticleAsync(string id);

    Task<int> UpdateArticleViewedAsync(string id);
}