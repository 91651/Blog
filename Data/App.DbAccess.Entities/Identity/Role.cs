using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }
    }
}
