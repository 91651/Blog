using System.ComponentModel;

namespace App.Business.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        [DisplayName("名称")]
        public string UserName { get; set; }
        [DisplayName("手机号")]
        public string PhoneNumber { get; set; }
        [DisplayName("邮箱")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
