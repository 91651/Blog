using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class UserLogin : UserLogin<string>
    {
    }
    public class UserLogin<TKey> : IdentityUserLogin<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
