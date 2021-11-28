using App.EFCore.DynamicLinq;

namespace App.Business.Services.Models
{
    public class ArticleQueryModel : Query
    {
        public string Id { get; set; }
        public string Keyword { get; set; }
        public string CreatedDate { get; set; }
        public string ChannelId { get; set; }
    }
}
