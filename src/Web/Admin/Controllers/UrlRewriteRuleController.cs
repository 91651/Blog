using Blog.Model;
using Blog.Service;
using EFCore.DynamicLinq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Admin.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "admin")]
[Area("admin")]
[Authorize]
[Route("api/[area]/[controller]")]
public class UrlRewriteRuleController : ControllerBase
{
    private readonly IUrlRewriteRuleService _urlRewriteRuleService;

    public UrlRewriteRuleController(IUrlRewriteRuleService urlRewriteRuleService)
    {
        _urlRewriteRuleService = urlRewriteRuleService;
    }

    [HttpPost]
    public Task<string> AddUrlRewriteRule(UrlRewriteRuleModel model)
    {
        return _urlRewriteRuleService.AddUrlRewriteRuleAsync(model);
    }

    [HttpDelete("{id}")]
    public Task<bool> DeleteUrlRewriteRule(string id)
    {
        return _urlRewriteRuleService.DeleteUrlRewriteRuleAsync(id);
    }

    [HttpPut("{id}")]
    public Task<bool> UpdateUrlRewriteRule(UrlRewriteRuleModel model)
    {
        return _urlRewriteRuleService.UpdateUrlRewriteRuleAsync(model);
    }

    [HttpGet]
    public Task<PageResult<List<UrlRewriteRuleModel>>> GetUrlRewriteRules([FromQuery] UrlRewriteRuleQueryModel model)
    {
        return _urlRewriteRuleService.GetUrlRewriteRulesAsync(model);
    }
}