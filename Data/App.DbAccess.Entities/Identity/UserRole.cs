using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class UserRole : UserRole<string>
    {
    }
    public class UserRole<TKey> : IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
