using Blog.Model;
using Blog.Service;
using EFCore.DynamicLinq;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Client.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "client")]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet("{id}")]
    public Task<ArticleModel> GetArticle(string id)
    {
        return _articleService.GetArticleAsync(id);
    }

    [HttpGet("{id}/prev")]
    public Task<ArticleModel> GetPrevArticle(string id)
    {
        return _articleService.GetPrevArticleAsync(id);
    }

    [HttpGet("{id}/next")]
    public Task<ArticleModel> GetNextArticleAsync(string id)
    {
        return _articleService.GetNextArticleAsync(id);
    }

    [HttpPut("{id}/viewed")]
    public Task UpdateArticleViewed(string id)
    {
        return _articleService.UpdateArticleViewedAsync(id);
    }

    [HttpGet]
    public Task<PageResult<List<ArticleListModel>>> GetArticles([FromQuery] ArticleQueryModel model)
    {
        var a = Request.HttpContext.Request.Path;
        return _articleService.GetArticlesAsync(model);
    }
}