using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace JKLSite.Models
{
    public class Login
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }   
        public int UserType { get; set; }
        public string errorMessage { get; set; }

        public bool Connect()
        {
            //Web.config файлаас өгөгдлийн сантай холбогдох String-ийг авч байна.
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            //Авсан тэмдэгтийн цуваагаар өгөгдлийн сантай холбох обьектыг үүсгэж байна.
            //Класс System.Data.SqlClient-д байгаа
            IDbConnection conn = new SqlConnection(dsn);
            //SQL комманд гүйцэтгэх обьектыг дээрх холбоосоос үүсгэж байна.
            IDbCommand cmd = conn.CreateCommand();
            //Коммандыг зааж өгж байна
            if (UserType == 1) { 
                cmd.CommandText = "SELECT * FROM admin where name='"+Name+"' AND password='"+Password+"' ";
            }
            if (UserType == 2)
            {
                cmd.CommandText = "SELECT * FROM Company where email='" + Name + "' AND password='" + Password + "' ";
            }
            try
            {
                //Класс System.Text-д байгаа
                StringBuilder sb = new StringBuilder();
                //Өгөгдлийн санг нээж байна
                conn.Open();
                //Коммандыг ажиллуулж байна
                IDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    return true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
            }
            finally
            {
                conn.Dispose();
            }
            return false;
        }
        public DataTable getCompany()
        {
                DataSet ds = new DataSet();
                using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
                {
                    //string SQL = "select * from products where id = @ProductID";
                    string SQL = "SELECT * FROM Company where email=@Name AND password=@Password";
                    Con.Open();


                    using (SqlCommand Com = new SqlCommand(SQL, Con))
                    {
                        Com.Parameters.Add(new SqlParameter("@Name", Name));
                        Com.Parameters.Add(new SqlParameter("@Password", Password));
                        using (SqlDataAdapter adap = new SqlDataAdapter(Com))
                        {
                            adap.Fill(ds);
                        }
                    }
                    Con.Dispose();
                }
            return ds.Tables[0];
        }
    }
}