using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Business.Model
{
    public class ChannelModel
    {
        [DisplayName("序号")]
        public string Id { get; set; }
        public string ParentId { get; set; }
        [DisplayName("标题")]
        [Required(ErrorMessage = "请输入用栏目标题")]
        public string Title { get; set; }
        [DisplayName("描述")]
        public string Description { get; set; }
        [DisplayName("状态")]
        public int State { get; set; }
    }
}
