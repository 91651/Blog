using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Entities.Identity;

public class Role : IdentityRole
{
    public Role()
    {
        Id = Guid.CreateVersion7().ToString();
    }
}