using Blog.Data.Entities.Identity;
using Blog.Model;
using Blog.Service;
using EFCore.DynamicLinq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Admin.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "admin")]
[Area("admin")]
[Authorize]
[Route("api/[area]/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IArticleService _articleService;

    public ArticleController(UserManager<User> userManager, SignInManager<User> signInManager, IArticleService articleService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _articleService = articleService;
    }

    [HttpPost]
    public Task<string> AddArticle(ArticleModel model)
    {
        model.UserId = _userManager.GetUserId(_signInManager.Context.User);
        model.OwnerId = _userManager.GetUserId(_signInManager.Context.User);
        return _articleService.AddArticleAsync(model);
    }

    [HttpDelete("{id}")]
    public Task<bool> DeleteArticle(string id)
    {
        return _articleService.DeleteArticleAsync(id);
    }

    [HttpPut("{id}")]
    public Task<bool> UpdateArticle(ArticleModel model)
    {
        return _articleService.UpdateArticleAsync(model);
    }

    [HttpGet("{id}")]
    public Task<ArticleModel> GetArticle(string id)
    {
        return _articleService.GetArticleAsync(id);
    }

    [HttpGet]
    public Task<PageResult<List<ArticleListModel>>> GetArticles([FromQuery] ArticleQueryModel model)
    {
        return _articleService.GetArticlesAsync(model);
    }
}