using Blog.Model;
using EFCore.DynamicLinq;

namespace Blog.Service
{
    public interface IUrlRewriteRuleService
    {
        Task<string> AddUrlRewriteRuleAsync(UrlRewriteRuleModel model);

        Task<bool> DeleteUrlRewriteRuleAsync(string id);

        Task<bool> UpdateUrlRewriteRuleAsync(UrlRewriteRuleModel model);

        Task<PageResult<List<UrlRewriteRuleModel>>> GetUrlRewriteRulesAsync(UrlRewriteRuleQueryModel model);
    }
}