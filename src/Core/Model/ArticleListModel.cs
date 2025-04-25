using System.ComponentModel;

namespace Blog.Model;

public class ArticleListModel
{
    [DisplayName("序号")]
    public string Id { get; set; }

    [DisplayName("标题")]
    public string Title { get; set; }

    [DisplayName("用户")]
    public string UserName { get; set; }

    public string ChannelId { get; set; }

    [DisplayName("栏目")]
    public string ChannelName { get; set; }

    [DisplayName("副标题")]
    public string SubTitle { get; set; }

    [DisplayName("概述")]
    public string Summary { get; set; }

    [DisplayName("浏览量")]
    public int Viewed { get; set; }

    [DisplayName("作者")]
    public string Author { get; set; }

    [DisplayName("评论量")]
    public int CommentCount { get; set; }

    [DisplayName("创建时间")]
    public DateTime Created { get; set; }

    [DisplayName("更新时间")]
    public DateTime Updated { get; set; }

    public int State { get; set; }
    public FileModel File { get; set; }
}