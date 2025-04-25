namespace Blog.Model;

public class ChannelTreeModel
{
    public ChannelModel Channel { get; set; }
    public ChannelTreeModel[] Children { get; set; }
}