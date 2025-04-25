using EFCore.DynamicLinq;

namespace Blog.Model;

public class UserQueryModel : Query
{
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}