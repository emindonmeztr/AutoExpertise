using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AutoExpertisePro.Models
{
    public class ExpertiseModels
    {
        //LoginModels içindeki değişkenlerin tanımlanması
        // Javascriptteki REGEX'ler C#'a uygulanırken başına ve sonuna / ifadesi gelmez!
        [Required(ErrorMessage = "Lütfen şase numaranızı belirtiniz.")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Şase numarası 17 karakterden oluşmalıdır!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Şase numarası sadece sayısal içerik içerebilir!")]
        public int sase_no { get; set; }

        [Required(ErrorMessage = "Lütfen bir müşteri belirleyiniz.")]
        //buraya seçim yapıldı mı kontrolü gelecek.
        public string musteri { get; set; }

        [Required(ErrorMessage = "Lütfen plakanızı belirleyiniz.")]
        //kullanıcıya format number-alphanumeric-numeric biçiminde gösterilmeli burada da kontrol yapılmalı
        public string plaka { get; set; }

        [Required(ErrorMessage = "Lütfen araç markası seçiniz!")]
        //buraya seçim yapıldı mı kontrolü gelecek.
        public string marka { get; set; }

        [Required(ErrorMessage = "Lütfen araç modeli seçiniz!")]
        //buraya seçim yapıldı mı kontrolü gelecek.
        public string model { get; set; }

        [Required(ErrorMessage = "Lütfen direksiyon tipi seçiniz!")]
        //buraya seçim yapıldı mı kontrolü gelecek.
        public string vitestipi { get; set; }

        [Required(ErrorMessage = "Lütfen yakıt türü seçiniz!")]
        //buraya seçim yapıldı mı kontrolü gelecek.
        public string yakittipi { get; set; }

        [Required(ErrorMessage = "Lütfen aracın rengini seçiniz!")]
        //buraya seçim yapıldı mı kontrolü gelecek.
        public string renk { get; set; }

        [Required(ErrorMessage = "Lütfen aracın kaç yılında üretildiğini seçiniz!")]
        //buraya seçim yapıldı mı kontrolü gelecek.
        public string modelyılı { get; set; }

        [Required(ErrorMessage = "Lütfen aracın motor hacmini belirleyiniz!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Motor hacmi sadece sayısal içerik içerebilir!")]
        public int motorhacmi { get; set; }

        [Required(ErrorMessage = "Lütfen aracın motor gücünü belirleyiniz!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Motor gücü sadece sayısal içerik içerebilir!")]
        public int motorgucu { get; set; }

        [Required(ErrorMessage = "Lütfen aracın kilometresini belirleyiniz!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Araç km değeri sadece sayısal içerik içerebilir!")]
        public int km { get; set; }


        [StringLength(256, MinimumLength = 3, ErrorMessage = "Araç sahibi ismi yada ünvanı minimum 3 ve maksimum 256 karakter olmalıdır!")]
        public string aracsahibi { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Araç sahibi cep telefonun no'su 10 karakterden oluşmalıdır!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Araç sahibi cep telefonun no'su sadece rakamlardan oluşmalıdır!")]
        public string aracsahibi_tel { get; set; }

    }
}