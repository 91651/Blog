using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
