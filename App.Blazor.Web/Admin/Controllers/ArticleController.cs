using App.Business.Model;
using App.Business.Services;
using App.DbAccess.Entities.Identity;
using App.EFCore.DynamicLinq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Admin.Controllers
{
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

        [HttpPost("query")]
        public Task<PageResult<List<ArticleListModel>>> GetArticles(ArticleQueryModel model)
        {
            return _articleService.GetArticlesAsync(model);
        }
    }
}
