using System.Text;
using App.Business.Model;
using App.Business.Services;
using Microsoft.AspNetCore.Mvc;
using X.Web.Sitemap;
using X.Web.Sitemap.Extensions;

namespace App.Blazor.Web.Client.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "client")]
    [Route("api/[controller]")]
    public class SeoController : ControllerBase
    {
        private readonly IChannelService _channelService;
        private readonly IArticleService _articleService;

        public SeoController(IChannelService channelService, IArticleService articleService)
        {
            _channelService = channelService;
            _articleService = articleService;
        }

        [HttpGet("/sitemap.xml")]
        public async Task<IActionResult> GenerateSiteMap()
        {
            var baseUrl = $"https://{Request.Host.Value}";
            var sitemap = new Sitemap();
            // home
            sitemap.Add(new Url
            {
                ChangeFrequency = ChangeFrequency.Yearly,
                Location = baseUrl,
                Priority = 1.0,
                TimeStamp = DateTime.Now.Date
            });
            // channel
            var channels = await _channelService.GetChannelsAsync();
            foreach (var channel in channels)
            {
                sitemap.Add(new Url
                {
                    ChangeFrequency = ChangeFrequency.Daily,
                    Location = $"{baseUrl}/c/{channel.Id}",
                    Priority = 0.8,
                    TimeStamp = DateTime.Now.Date
                });
            }
            // page
            foreach (var channel in channels)
            {
                var query = new ArticleQueryModel { ChannelId = channel.Id };
                var articles = await _articleService.GetArticlesAsync(query);
                foreach (var article in articles.Data)
                {
                    sitemap.Add(new Url
                    {
                        ChangeFrequency = ChangeFrequency.Weekly,
                        Location = $"{baseUrl}/p/{article.Id}",
                        Priority = 0.5,
                        TimeStamp = article.Updated
                    });
                }
            }
            var xml = sitemap.ToXml();
            return Content(xml, "application/xml", Encoding.UTF8);
        }
    }
}
