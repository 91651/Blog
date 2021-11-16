using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class RoleClaim : RoleClaim<string>
    {
    }
    public class RoleClaim<TKey> : IdentityRoleClaim<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
