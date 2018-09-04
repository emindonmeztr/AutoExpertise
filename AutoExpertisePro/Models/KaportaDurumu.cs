using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AutoExpertisePro.Models
{
    public class KaportaDurumu
    {
        //LoginModels içindeki değişkenlerin tanımlanması
        // Javascriptteki REGEX'ler C#'a uygulanırken başına ve sonuna / ifadesi gelmez!
        
        public string tampon_arka { get; set; }
        public string tampon_on { get; set; }

        public string kaput { get; set; }

        public string camurluk_on_sag { get; set; }

        public string camurluk_on_sol { get; set; }

        public string tavan { get; set; }

        public string bagaj_kapagi { get; set; }

        public string camurluk_arka_sag { get; set; }

        public string camurluk_arka_sol { get; set; }

        public string kapi_arka_sol { get; set; }

        public string kapi_arka_sag { get; set; }

        public string kapi_on_sag { get; set; }

        public string kapi_on_sol { get; set; }

    }
}