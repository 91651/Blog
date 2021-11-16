using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class Role : Role<string>
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class Role<TKey> : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
