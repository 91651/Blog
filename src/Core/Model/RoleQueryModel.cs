using EFCore.DynamicLinq;

namespace Blog.Model;

public class RoleQueryModel : Query
{
    public string Name { get; set; }
}