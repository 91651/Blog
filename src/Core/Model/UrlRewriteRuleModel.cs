using System.ComponentModel;

namespace Blog.Model;

public class UrlRewriteRuleModel
{
    public string Id { get; set; }

    [DisplayName("源地址")]
    public string Regex { get; set; }

    [DisplayName("目标地址")]
    public string Replacement { get; set; }

    [DisplayName("状态")]
    public string StatusCode { get; set; }

    public int Status { get; set; }
}