using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Data.Entities.Identity;
using Util;

namespace Blog.Data.Entities;

public class Article
{
    [Key]
    [MaxLength(40)]
    public string Id { get; set; } = Guid.CreateVersion7().ToString(10);

    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(200)]
    public string SubTitle { get; set; }

    [MaxLength(500)]
    public string Summary { get; set; }

    [MaxLength(40)]
    public string UserId { get; set; }

    [MaxLength(40)]
    public string OwnerId { get; set; }

    [MaxLength(40)]
    public string ChannelId { get; set; }

    [MaxLength(60)]
    public string Author { get; set; }

    public int? Editor { get; set; }

    [MaxLength]
    public string Content { get; set; }

    [MaxLength]
    public string MdContent { get; set; }

    [DefaultValue(0)]
    public int Viewed { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public int State { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("ChannelId")]
    public Channel Channel { get; set; }

    [ForeignKey("OwnerId")]
    public virtual ICollection<Comment> Comments { get; } = new HashSet<Comment>();

    [ForeignKey("OwnerId")]
    public virtual ICollection<File> Files { get; } = new HashSet<File>();
}