using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class UserToken : UserToken<string>
    {
    }
    public class UserToken<TKey> : IdentityUserToken<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
