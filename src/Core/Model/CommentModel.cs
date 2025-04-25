using System.ComponentModel.DataAnnotations;

namespace Blog.Model;

public class CommentModel
{
    public string Id { get; set; }
    public string ParentId { get; set; }
    public string OwnerId { get; set; }

    [Required(ErrorMessage = "请输入一个心仪的昵称")]
    [MinLength(3, ErrorMessage = "昵称至少3个字符")]
    [MaxLength(20, ErrorMessage = "昵称最大长度20个字符")]
    public string Author { get; set; }

    [Required(ErrorMessage = "请输入有效的邮箱地址")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "请输入想留下的话语")]
    [MinLength(5, ErrorMessage = "评论内容至少5个字符")]
    [MaxLength(500, ErrorMessage = "评论内容最大长度500个字符")]
    public string Content { get; set; }

    public DateTime Created { get; set; }
    public List<CommentModel> Comments { get; set; }
}