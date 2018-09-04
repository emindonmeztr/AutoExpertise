using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace AutoExpertisePro.Models
{
    public class DatabaseModel
    {
        public int veri { get; set; }
        public string email { get; set; }

        private string password { get; set; }
        public string phone { get; set; }
        public bool assertEmailAvailable;
        public bool assertPhoneAvailable;
        private bool connection_open;
        private MySqlConnection connection;

        public DatabaseModel()
        {

        }

        public DatabaseModel(int arg_id)  //İLK DENEME KODUM SIMPLE CODE
        {
            Get_Connection();
            veri = arg_id;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    string.Format("INSERT INTO tablo(veri)   VALUES (@param1) ;");
                cmd.Parameters.AddWithValue("@param1", veri);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
            }
            catch (MySqlException e)
            {
                string MessageString = "Read error occurred  / entry not found loading the Column details: "
                + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
            }
        }

        public DatabaseModel(string _email) // EMAİL KULLANILIYOR MU?
        {
            Get_Connection();
            email = _email;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                string.Format("SELECT id FROM customersinfo WHERE email = (@param1);");
                cmd.Parameters.AddWithValue("@param1", email);
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows) // Eğer değer 1'e eşit veya büyükse veritabanında aynı email kullanılmış demektir.
                    assertEmailAvailable = false; // kullanılamaz
                else
                {
                   assertEmailAvailable = true; // kullanılabilir
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                string MessageString = "Read error occurred  / entry not found loading the Column details: "
                + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
                assertEmailAvailable = false;
            }
        }

        public DatabaseModel(string _phone,bool _ayristici) // TELEFON NUMARASI KULLANILIYOR MU?
        {
            Get_Connection();
            phone = _phone;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                string.Format("SELECT id FROM customersinfo WHERE tlf_no = (@param1);");
                cmd.Parameters.AddWithValue("@param1", phone);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) // Eğer değer 1'e eşit veya büyükse veritabanında aynı telefon numarası kullanılmış demektir.
                    assertPhoneAvailable = false; // kullanılamaz
                else
                {
                    assertPhoneAvailable = true; // kullanılabilir
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                string MessageString = "Read error occurred  / entry not found loading the Column details: "
                + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
                assertPhoneAvailable = false;
            }
        }

        public DatabaseModel(string param1,string param2,string param3,string param4, string param5) // KAYIT İŞLEMİ
        {
            Get_Connection();
            string Email = param1;
            string Password = param2;
            string İsimSoyisim = param3;
            string CepTelefonu = param4;
            string Adres;
            if (!String.IsNullOrEmpty(param5)){
                Adres = param5;
            }
            else { 
            Adres = null;
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                string.Format("INSERT INTO customersinfo(email,password,isim,tlf_no,adres)   VALUES (@param1,@param2,@param3,@param4,@param5) ;");
                cmd.Parameters.AddWithValue("@param1", Email);
                cmd.Parameters.AddWithValue("@param2", Password);
                cmd.Parameters.AddWithValue("@param3", İsimSoyisim);
                cmd.Parameters.AddWithValue("@param4", CepTelefonu);
                cmd.Parameters.AddWithValue("@param5", Adres);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Close();
            }
            catch (MySqlException e)
            {
                string MessageString = "Read error occurred  / entry not found loading the Column details: "
                + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
            }
        }

        private void Get_Connection()
        {
            connection_open = false;

            connection = new MySqlConnection();
            //connection = DB_Connect.Make_Connnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;

            //            if (db_manage_connnection.DB_Connect.OpenTheConnection(connection))
            if (Open_Local_Connection())
            {
                connection_open = true;
            }
            else
            {
                //					MessageBox::Show("No database connection connection made...\n Exiting now", "Database Connection Error");
                //					 Application::Exit();
            }

        }


        private bool Open_Local_Connection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}