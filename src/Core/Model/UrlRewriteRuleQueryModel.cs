using EFCore.DynamicLinq;

namespace Blog.Model;

public class UrlRewriteRuleQueryModel : Query
{
    public string Regex { get; set; }
    public string Replacement { get; set; }
    public string StatusCode { get; set; }
}