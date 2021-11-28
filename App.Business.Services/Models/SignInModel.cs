using System.ComponentModel.DataAnnotations;

namespace App.Business.Services.Models
{
    public class SignInModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Pwd { get; set; }
    }
}
