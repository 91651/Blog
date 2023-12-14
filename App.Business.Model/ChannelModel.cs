using System.ComponentModel;

namespace App.Business.Model
{
    public class ChannelModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        [DisplayName("标题")]
        public string Title { get; set; }
        [DisplayName("描述")]
        public string Description { get; set; }
        [DisplayName("状态")]
        public int State { get; set; }
    }
}
