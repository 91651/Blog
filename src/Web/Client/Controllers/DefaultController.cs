using System.Text;
using AntDesign;
using Blog.Data;
using Blog.Model;
using Blog.Service;
using Microsoft.AspNetCore.Mvc;
using X.Web.Sitemap;
using X.Web.Sitemap.Extensions;

namespace Blog.Web.Client.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "client")]
[Route("api/[controller]")]
public class DefaultController(AppDbContext _db, IChannelService channelService, IArticleService articleService) : ControllerBase
{

    [HttpGet("/api/stats")]
    public async Task<StatsModel> GetSiteStats(string tz)
    {
        var timeZone = TimeZoneInfo.TryFindSystemTimeZoneById(tz, out var tzInfo) ? tzInfo : TimeZoneInfo.Utc;
        var localNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        var utcNow = localNow.Date.ToUniversalTime();
        var viewCount = _db.UserTraces.Count(p => p.StartTime > utcNow);
        var statistics = new StatsModel { ViewCount = viewCount };
        return statistics;
    }

    [HttpGet("/api/ping")]
    public async Task<IActionResult> Ping(CancellationToken cancellationToken = default)
    {
        Response.ContentType = "text/event-stream";
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("Connection", "keep-alive");
        Response.Headers.Append("X-Accel-Buffering", "no");
        while (true)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                await Response.WriteAsync($"data: {DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}\n\n", cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
                await Task.Delay(2000, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
        return Ok();
    }

    [HttpGet("/sitemap.xml")]
    public async Task<IActionResult> GenerateSiteMap()
    {
        var baseUrl = $"https://{Request.Host.Value}";
        var sitemap = new Sitemap();
        // home
        sitemap.Add(new Url
        {
            ChangeFrequency = ChangeFrequency.Daily,
            Location = baseUrl,
            Priority = 1.0,
            TimeStamp = DateTime.Now.Date
        });
        // channel
        var channels = await channelService.GetChannelsAsync();
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
            var query = new ArticleQueryModel { ChannelId = channel.Id, PageSize = int.MaxValue };
            var articles = await articleService.GetArticlesAsync(query);
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