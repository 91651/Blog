using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Business.Model
{
    public class ArticleModel
    {
        public string Id { get; set; }
        [DisplayName("标题")]
        [Required(ErrorMessage = "请输入用文章标题")]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Summary { get; set; }
        public string UserId { get; set; }
        public string OwnerId { get; set; }
        [DisplayName("栏目")]
        [Required(ErrorMessage = "请选择文章所属栏目")]
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string Author { get; set; }
        public int Editor { get; set; }
        public string Content { get; set; }
        public string MdContent { get; set; }
        public int Viewed { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int? State { get; set; }
        public string[] Files { get; set; }
    }
}
