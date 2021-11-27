using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class Role : IdentityRole
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
