using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Entities.Identity;

public class User : IdentityUser
{
    public User()
    {
        Id = Guid.NewGuid().ToString();
    }
}