using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoExpertisePro.Models;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace AutoExpertisePro.Controllers
{
    public class HomeController : Controller
    {
        DatabaseModel deneme = new DatabaseModel(123);
        private MySqlConnection connection;

        public ActionResult anasayfa()
        {
            Debug.WriteLine(deneme.veri);
            return View();
        }

       /* public class Yeni
        {

            public void deneme() { 
            RegisterModels rg = new RegisterModels();
                rg.
            }
        }*/


        [HttpPost]
        public ActionResult login(LoginModels login)
        {
            try
            {
                if ((!String.IsNullOrEmpty(login.Email)) && (!String.IsNullOrEmpty(login.Password)))// Null or empty controller
                {
                    string Email = login.Email;
                    string Password = login.Password;
                    string yanit = ReDecryptAndControlLogin(Email, Password);
                    if (yanit == "Doğru") { ViewData["loginmsg1"] = "Sisteme başarıyla giriş yaptınız!"; return View("anasayfa"); } // pass true
                    else if (yanit == "Yanlış" || yanit == "Email Yok") { ViewData["loginmsg2"] = "Gecersiz email adresi veya şifre!"; return View("login"); } // pass wrong
                    else
                    {
                        ViewData["loginmsg3"] = "Sisteme giriş yaparken hata oluştu!";
                        return View("login");
                    }
                }
                else {
                    ViewData["loginmsg4"] = "Email ve şifre alanı boş bırakılamaz!";
                    return View("login");
                }
            }
            catch (Exception e)
            {
                ViewData["loginmsg5"] = "Sisteme giriş yaparken hata oluştu!";
                return View("login");
            }
      }
      

        [HttpPost]  // post ederken [HttpPost] ön eki getirilmeli!!
        public ActionResult anasayfa(RegisterModels register) //Models>>RegisterModels.cs isimli model class'ını Controller'a parametre olarak gönderiyorum.
        {
            try {
            if ((!String.IsNullOrEmpty(register.Email)) && (!String.IsNullOrEmpty(register.Password)) && (!String.IsNullOrEmpty(register.CepTelefonu)) && (!String.IsNullOrEmpty(register.İsimSoyisim)))  // Null or empty controller
            {
                string Email = register.Email;
                string Password = register.Password;
                bool tempcontrol1 = checkEmail(Email);
                if (tempcontrol1 == false) // email kontrol fonksiyonundan dönen değere göre işlem yap
                    {
                        ViewData["message1"] = "Girdiğiniz email adresi = " + Email + " başka bir kullanıcı tarafından kullanılmıştır!";
                        return View("kaydol");
                    }
                    else { // geçerliyse devam et
                        string donendeger = Encrypt(register.Password);
                        Password = donendeger;
                        string İsimSoyisim = register.İsimSoyisim;
                string CepTelefonu = register.CepTelefonu;
                bool tempcontrol2 = checkPhone(CepTelefonu);
                if (tempcontrol2 == false) // telefon numarası kontrol fonksiyonundan dönen değere göre işlem yap
                {
                            ViewData["message2"] = "Girdiğiniz telefon numarası = " + CepTelefonu + " başka bir kullanıcı tarafından kullanılmıştır!";
                            return View("kaydol");
                        }
                        else
                        { // geçerliyse devam et
                string Adres;
                if (!String.IsNullOrEmpty(register.Adres)){
                    Adres = register.Adres;
                }
                else
                {
                    Adres = null;
                }

                DatabaseModel kaydol = new DatabaseModel(Email, Password, İsimSoyisim, CepTelefonu, Adres);//send parameters
                            ViewData["isimsoyisim"] = İsimSoyisim;
                            ViewData["email"] = Email;
                            return View("kayitbasarili");
                }
            }
                }
                else {
                    ViewData["message3"] = "Lütfen zorunlu alanları doldurunuz!";
                    return View("kaydol");
                }
            }
            catch(Exception e) {
                ViewBag.Message = e.InnerException;
                ViewData["message4"] = "Sunucuda bir problem oluştu! Daha sonra tekrar deneyiniz! ";
                return View("kaydol");
            }
        }

        private bool checkEmail(string email)
        {
            DatabaseModel checkemail = new DatabaseModel(email);
            if (checkemail.assertEmailAvailable == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Encrypt(string plainText)
        {
            try { 
            if (plainText == null) throw new ArgumentNullException("plainText");

            //encrypt data
            var data = Encoding.Unicode.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

            //return as base64 string
            return Convert.ToBase64String(encrypted);
            }
            catch(Exception e)
            {
                return "Hata";
            }
        }

        public string Decrypt(string cipher)
        {
            if (cipher == null) throw new ArgumentNullException("cipher");

            //parse base64 string
            byte[] data = Convert.FromBase64String(cipher);

            //decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decrypted);
        }

        public string ReDecryptAndControlLogin(string email, string enteredpass)
        {
            try
            {
                connection = new MySqlConnection();
                //connection = DB_Connect.Make_Connnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                string.Format("SELECT password FROM customersinfo WHERE email = (@param1);");
                cmd.Parameters.AddWithValue("@param1", email);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) // Eğer değer 1'e eşit veya büyükse veritabanında ilgili email kullanılmış demektir.
                {
                    string ColumnName ="";
                    while (reader.Read())
                    {
                        ColumnName = (string)reader["password"];
                    }

                    byte[] data = Convert.FromBase64String(ColumnName);
                    //decrypt data
                    byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
                    if (Encoding.Unicode.GetString(decrypted).ToString() == enteredpass)
                    { return "Doğru"; }
                    else
                    { return "Yanlış"; }
                }
                else
                {
                    return "Email Yok";
                }
            }
            catch(Exception e) {
                return e.Message;
            }
        }

        private bool checkPhone(string Phone)
        {
            bool ayristirici = false; //overloading işlemi için ayrıştırma
            DatabaseModel checkphone = new DatabaseModel(Phone, ayristirici);
            if ((checkphone.assertPhoneAvailable == true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult login()
        {
            return View();
        }


        public ActionResult denemeyeni()
        {
            return View();
        }
        public ActionResult kayitbasarili()
        {
            return View();
        }

        public ActionResult kaydol()
        {
            return View();
        }

        public ActionResult bildirimler()
        {
            return View();
        }

        public ActionResult ekspertizolustur()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ekspertizolustur(ExpertiseModels exp)
        {
            try {
                DateTime dateTime = DateTime.Now;
                connection = new MySqlConnection();
            //connection = DB_Connect.Make_Connnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText =
            string.Format("INSERT INTO processesbase(sase_no,musteri_id,tarih,plaka,marka,model,vites_tipi,renk,yakit_tipi,model_yili,motor_hacmi,motor_gucu,km,aracsahibi,aracsahibi_tlf)   VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11,@param12,@param13,@param14,@param15);");
            cmd.Parameters.AddWithValue("@param1", exp.sase_no);
            cmd.Parameters.AddWithValue("@param2", 1);
            cmd.Parameters.AddWithValue("@param3", dateTime.ToString());
            cmd.Parameters.AddWithValue("@param4", exp.plaka);
            cmd.Parameters.AddWithValue("@param5", exp.marka);
                cmd.Parameters.AddWithValue("@param6", exp.model);
                cmd.Parameters.AddWithValue("@param7", exp.vitestipi);
                cmd.Parameters.AddWithValue("@param8", exp.renk);
                cmd.Parameters.AddWithValue("@param9", exp.yakittipi);
                cmd.Parameters.AddWithValue("@param10", exp.modelyılı);
                cmd.Parameters.AddWithValue("@param11", exp.motorhacmi);
                cmd.Parameters.AddWithValue("@param12", exp.motorgucu);
                cmd.Parameters.AddWithValue("@param13", exp.km);
                cmd.Parameters.AddWithValue("@param14", exp.aracsahibi);
                cmd.Parameters.AddWithValue("@param15", exp.aracsahibi_tel);
                MySqlDataReader reader = cmd.ExecuteReader();
                return View("kaportadurumu");
            reader.Close();
            }
            catch(MySqlException e)
            {
                string MessageString = "Read error occurred  / entry not found loading the Column details: "
               + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
                return View(); }
        }

        public ActionResult kaportadurumu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult kaportadurumu(KaportaDurumu kap)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                connection = new MySqlConnection();
                //connection = DB_Connect.Make_Connnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;

                cmd.CommandText =
                string.Format("INSERT INTO paintingandchanging(process_id,date,tampon_arka,tampon_on,kaput,camurluk_on_sag,camurluk_on_sol,tavan,bagaj_kapagi,camurluk_arka_sag,camurluk_arka_sol,kapi_arka_sol,kapi_arka_sag,kapi_on_sag,kapi_on_sol)   VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11,@param12,@param13,@param14,@param15);");
                cmd.Parameters.AddWithValue("@param1", 1);
                cmd.Parameters.AddWithValue("@param2", dateTime.ToString());
                cmd.Parameters.AddWithValue("@param3", ParcaDegerConvert(kap.tampon_arka));
                cmd.Parameters.AddWithValue("@param4", ParcaDegerConvert(kap.tampon_on));
                cmd.Parameters.AddWithValue("@param5", ParcaDegerConvert(kap.kaput));
                cmd.Parameters.AddWithValue("@param6", ParcaDegerConvert(kap.camurluk_on_sag));
                cmd.Parameters.AddWithValue("@param7", ParcaDegerConvert(kap.camurluk_on_sol));
                cmd.Parameters.AddWithValue("@param8", ParcaDegerConvert(kap.tavan));
                cmd.Parameters.AddWithValue("@param9", ParcaDegerConvert(kap.bagaj_kapagi));
                cmd.Parameters.AddWithValue("@param10", ParcaDegerConvert(kap.camurluk_arka_sag));
                cmd.Parameters.AddWithValue("@param11", ParcaDegerConvert(kap.camurluk_arka_sol));
                cmd.Parameters.AddWithValue("@param12", ParcaDegerConvert(kap.kapi_arka_sol));
                cmd.Parameters.AddWithValue("@param13", ParcaDegerConvert(kap.kapi_arka_sag));
                cmd.Parameters.AddWithValue("@param14", ParcaDegerConvert(kap.kapi_on_sag));
                cmd.Parameters.AddWithValue("@param15", ParcaDegerConvert(kap.kapi_on_sol));
                MySqlDataReader reader = cmd.ExecuteReader();

                ViewData["basarili"] = "Kayit basarili!";
                return View("kaportadurumu");
                reader.Close();
            }
            catch (MySqlException e)
            {
                string MessageString = "Read error occurred  / entry not found loading the Column details: "
               + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
                ViewData["basarisiz"] = "Kayit basarisiz!";
                return View();
            }
        }

        public int ParcaDegerConvert(string param)
        {
            if (param == "Boyalı")
            { return 1; }

            else if (param == "Değişen")
            { return 2; }

            else if (param == "Orjinal")
            { return 0; }

            else return -1;

            
        }

        public ActionResult aracraporlari()
        {
            return View();
        }

        public ActionResult ekspertizislemlerim()
        {
            return View();
        }

        
    }
}