using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Business.Model
{
    public class ChannelTreeModel
    {
        public ChannelModel Channel { get; set; }
        public ChannelTreeModel[] Children { get; set; }
    }
}
