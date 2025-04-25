using System.ComponentModel.DataAnnotations;

namespace Blog.Model;

public class SignInModel
{
    [Required(ErrorMessage = "请输入用户名")]
    public string Name { get; set; }

    [Required(ErrorMessage = "请输入用户密码")]
    public string Pwd { get; set; }
}