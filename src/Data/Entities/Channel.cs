using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities;

public class Channel
{
    [Key]
    [MaxLength(40)]
    public string Id { get; set; } = Guid.CreateVersion7().ToString();

    [MaxLength(40)]
    public string ParentId { get; set; }

    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }

    public int State { get; set; }

    public virtual ICollection<Article> Articles { get; } = new List<Article>();
}