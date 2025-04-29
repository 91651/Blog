namespace Blog.Data.Entities;

public class UrlRewriteRule
{
    public string Id { get; set; }
    public string Regex { get; set; }
    public string Replacement { get; set; }
    public string StatusCode { get; set; }
    public int Status { get; set; }
}