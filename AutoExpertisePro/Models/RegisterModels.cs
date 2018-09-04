using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AutoExpertisePro.Models
{
    public class RegisterModels
    {
        //LoginModels içindeki değişkenlerin tanımlanması
        // Javascriptteki REGEX'ler C#'a uygulanırken başına ve sonuna / ifadesi gelmez!
        [Required(ErrorMessage = "Lütfen email adresinizi belirtiniz.")]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Email adresiniz minimum 6 ve maksimum 256 karakterden oluşmalıdır!")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Geçersiz bir email adresi girdiniz! Lütfen email adresinizi kontrol ediniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen bir şifre belirleyiniz.")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "Şifre alanı minimum 8 ve maksimum 128 karakterden oluşmalıdır!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen isminizi veya ünvanınızı belirtiniz.")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "İsminiz yada ünvanınız minimum 3 ve maksimum 256 karakter olmalıdır!")]
        public string İsimSoyisim { get; set; }

        [Required(ErrorMessage = "Lütfen cep telefon numaranızı giriniz!")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Cep telefonun numaranız 10 karakterden oluşmalıdır!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Cep telefonu numaranız sadece rakamlardan oluşmalıdır!")]
        public string CepTelefonu { get; set; }

        [StringLength(256, MinimumLength = 3, ErrorMessage = "Adres bilginiz minimum 3 ve maksimum 256 karakterden oluşmalıdır!")]
        public string Adres { get; set; }
    }
}