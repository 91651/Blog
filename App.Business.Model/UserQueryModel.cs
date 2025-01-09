using App.EFCore.DynamicLinq;

namespace App.Business.Model
{
    public class UserQueryModel : Query
    {
        public string Name { get; set; }
    }
}
