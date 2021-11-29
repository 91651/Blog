using App.EFCore.DynamicLinq;

namespace App.Business.Model
{
    public class ArticleQueryModel : Query
    {
        public string? Id { get; set; }
        public string? Keyword { get; set; }
        public string? CreatedDate { get; set; }
        public string? ChannelId { get; set; }
    }
}
