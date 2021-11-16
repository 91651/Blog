using Microsoft.AspNetCore.Identity;

namespace App.DbAccess.Entities.Identity
{
    public class User : User<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class User<TKey> : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
