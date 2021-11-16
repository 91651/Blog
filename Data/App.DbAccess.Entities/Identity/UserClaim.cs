using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class UserClaim : UserClaim<string>
    {
    }
    public class UserClaim<TKey> : IdentityUserClaim<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
