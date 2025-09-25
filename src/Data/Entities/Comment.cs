using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entities;

public class Comment
{
    [Key]
    [MaxLength(40)]
    public string Id { get; set; } = Guid.CreateVersion7().ToString();

    [MaxLength(40)]
    public string ParentId { get; set; }

    [MaxLength(40)]
    public string OwnerId { get; set; }

    [MaxLength(60)]
    public string Author { get; set; }

    [MaxLength(200)]
    public string Email { get; set; }

    [MaxLength]
    public string Content { get; set; }

    public DateTime Created { get; set; }
}