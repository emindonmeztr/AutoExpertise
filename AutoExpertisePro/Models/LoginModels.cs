using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AutoExpertisePro.Models
{
    public class LoginModels
    {
        //LoginModels içindeki değişkenlerin tanımlanması
        // Javascriptteki REGEX'ler C#'a uygulanırken başına ve sonuna / ifadesi gelmez!
        [Required(ErrorMessage = "Please enter an email address.")]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Email field must be at least 6 and maximum 256 characters.")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "You entered an invalid email address! Please provide a valid email address..")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "Password field must be at least 8 and maximum 128 characters.")]
        public string Password { get; set; }
    }
}