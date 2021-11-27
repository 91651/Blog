using System.ComponentModel.DataAnnotations;

namespace App.Blazor.Admin.Model
{
    public class SignInModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Pwd { get; set; }
    }
}
