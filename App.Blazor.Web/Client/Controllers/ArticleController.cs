using App.Business.Model;
using App.Business.Services;
using App.DbAccess.Entities.Identity;
using App.EFCore.DynamicLinq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Blazor.Web.Client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public Task<ArticleModel> GetArticle(string id)
        {
            return _articleService.GetArticleAsync(id);
        }

        [HttpGet("prev")]
        public Task<ArticleModel> GetPrevArticle(string id, string chennelId)
        {
            return _articleService.GetPrevArticleAsync(id, chennelId);
        }

        [HttpGet("next")]
        public Task<ArticleModel> GetNextArticleAsync(string id, string chennelId)
        {
            return _articleService.GetNextArticleAsync(id, chennelId);
        }

        [HttpPut("{id}/viewed")]
        public Task UpdateArticleViewed(string id)
        {
            return _articleService.UpdateArticleViewedAsync(id);
        }

        [HttpGet]
        public Task<PageResult<List<ArticleListModel>>> GetArticles([FromQuery]ArticleQueryModel model)
        {
            return _articleService.GetArticlesAsync(model);
        }
    }
}
