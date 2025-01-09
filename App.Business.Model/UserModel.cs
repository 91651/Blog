using System.ComponentModel;

namespace App.Business.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        [DisplayName("名称")]
        public string UserName { get; set; }
    }
}
