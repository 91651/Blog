using System.ComponentModel;

namespace Blog.Model;

public class RoleModel
{
    public string Id { get; set; }

    [DisplayName("名称")]
    public string Name { get; set; }
}