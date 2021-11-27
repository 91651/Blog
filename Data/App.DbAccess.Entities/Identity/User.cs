using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class User : IdentityUser
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
