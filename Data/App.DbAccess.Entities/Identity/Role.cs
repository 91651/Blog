using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class Role : IdentityRole<string>
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
