using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JKLSite.Models
{
    public class JKLModel
    {
        public static string sailorName { get; set; }
        public static DateTime dateOfBirth { get; set; }
        public static int maritialStatus { get; set; }
        public static string address { get; set; }
        public static double height { get; set; }
        public static double weight { get; set; }
        public static string bloodType { get; set; }
        public static int shoeSize { get; set; }
        public static int jobStatus { get; set; }
        public static List<SelectListItem> items = new List<SelectListItem>();
        public static SelectListItem[] getMaritialStatus()
        {
            items.Clear();
            items.Add(new SelectListItem { Text = "Гэрлэсэн", Value = "1" });
            items.Add(new SelectListItem { Text = "Гэрлээгүй", Value = "2" });
            items.Add(new SelectListItem { Text = "Салсан", Value = "3" });
            return items.ToArray();
        }
        public static SelectListItem[] getBloodType()
        {
            items.Clear();
            items.Add(new SelectListItem { Text = "O+", Value = "O+" });
            items.Add(new SelectListItem { Text = "O–", Value = "O–" });
            items.Add(new SelectListItem { Text = "A+", Value = "A+" });
            items.Add(new SelectListItem { Text = "A-", Value = "A-" });
            items.Add(new SelectListItem { Text = "B+", Value = "B+" });
            items.Add(new SelectListItem { Text = "B-", Value = "B-" });
            items.Add(new SelectListItem { Text = "AB+", Value = "AB+" });
            items.Add(new SelectListItem { Text = "AB-", Value = "AB-" });
            return items.ToArray();
        }
        public static SelectListItem[] getJobStatus()
        {
            items.Clear();
                //GetDataSet
                DataSet ds = new DataSet();
                DataRow newRow = null;
                using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
                {
                    //string SQL = "select * from products where id = @ProductID";
                    string SQL = "select * from JobStatus";
                    Con.Open();


                    using (SqlCommand Com = new SqlCommand(SQL, Con))
                    {
                        //Com.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                        using (SqlDataAdapter adap = new SqlDataAdapter(Com))
                        {
                            adap.Fill(ds);
                        }
                    }
                }

            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                items.Add(new SelectListItem { Text = dr["JobId"].ToString()+"-"+dr["NameMon"].ToString(), Value = dr["JobId"].ToString() });
            }

            return items.ToArray();
        }

        public static string SailorAdd()
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("AddSailor", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@SailorName ",sailorName );
            cmd.Parameters.AddWithValue("@DateOfBirth ", dateOfBirth);
            cmd.Parameters.AddWithValue("@MaritialStatus ", maritialStatus);
            cmd.Parameters.AddWithValue("@Address ", address);
            cmd.Parameters.AddWithValue("@Height ", height);
            cmd.Parameters.AddWithValue("@Weight ", weight);
            cmd.Parameters.AddWithValue("@BloodType ", bloodType);
            cmd.Parameters.AddWithValue("@ShoeSize ", shoeSize);
            cmd.Parameters.AddWithValue("@JobStatus ", jobStatus);
            //Процедурын гаралтын параметрийг дараахь байдлаар үүсгэн холбож өгнө
            SqlParameter idParam = new SqlParameter("@SailorId ", SqlDbType.Int);
            idParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(idParam);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                return ex.ToString();
            }
            finally
            {
                conn.Dispose();
            }
            return "Амжилттай нэмэгдлээ";
        }
       
    }
}