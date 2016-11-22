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
        public static int ServiceSailorId { get; set; }
        public static int ServiceRankId { get; set; }
        public static int ServiceVesselId { get; set; }
        public static DateTime ServiceSignOnDate { get; set; }
        public static DateTime ServiceSignOfDate { get; set; }
        public static int ServiceContractPerion { get; set; }
        public static DateTime ServiceContractEndDate { get; set; }

        public static string CompanyName{ get; set; }
        public static string CompanyContactPerson { get; set; }
        public static string CompanyEmail { get; set; }
        public static string CompanyPhone { get; set; }
        public static string CompanyAddress { get; set; }
        public static string CompanyPassword { get; set; }
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
                Con.Dispose();
                }

            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                items.Add(new SelectListItem { Text = dr["JobId"].ToString()+"-"+dr["NameMon"].ToString(), Value = dr["JobId"].ToString() });
            }

            return items.ToArray();
        }

        public static SelectListItem[] getSailor()
        {
            items.Clear();
            //GetDataSet
            DataSet ds = new DataSet();
            using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
            {
                //string SQL = "select * from products where id = @ProductID";
                string SQL = "select * from Sailor where JobStatus=1 OR JobStatus=4";
                Con.Open();


                using (SqlCommand Com = new SqlCommand(SQL, Con))
                {
                    //Com.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                    using (SqlDataAdapter adap = new SqlDataAdapter(Com))
                    {
                        adap.Fill(ds);
                    }
                }
                Con.Dispose();
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                items.Add(new SelectListItem { Text = dr["SailorId"].ToString() + "-" + dr["SailorName"].ToString(), Value = dr["SailorId"].ToString() });
            }

            return items.ToArray();
        }
        public static SelectListItem[] getRank()
        {
            items.Clear();
            //GetDataSet
            DataSet ds = new DataSet();
            using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
            {
                //string SQL = "select * from products where id = @ProductID";
                string SQL = "select * from Rank";
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

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                items.Add(new SelectListItem { Text = dr["RankId"].ToString() + "-" + dr["DescMon"].ToString(), Value = dr["RankId"].ToString() });
            }

            return items.ToArray();
        }
        public static SelectListItem[] getVessel(int ?Id)
        {
            string SQL;
            items.Clear();
            //GetDataSet
            DataSet ds = new DataSet();
            using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
            {
                if (Id != null)
                {
                    //string SQL = "select * from products where id = @ProductID";
                     SQL = @"select v.*,c.CompanyName from Vessel as v
                                left join Company as c on v.CompanyId = c.CompanyId where v.CompanyId=@comId";
                }
                else
                { //string SQL = "select * from products where id = @ProductID";
                     SQL = @"select v.*,c.CompanyName from Vessel as v
                                left join Company as c on v.CompanyId = c.CompanyId";
                }
                Con.Open();


                using (SqlCommand Com = new SqlCommand(SQL, Con))
                {
                    if(Id!=null)    Com.Parameters.Add(new SqlParameter("@comId", Id));
                    using (SqlDataAdapter adap = new SqlDataAdapter(Com))
                    {
                        adap.Fill(ds);
                    }
                }
                Con.Dispose();
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                items.Add(new SelectListItem { Text = dr["CompanyName"].ToString() + "-" + dr["VesselName"].ToString(), Value = dr["VesselId"].ToString() });
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
            return "Далайчин амжилттай нэмэгдлээ";
        }

        public static string CompanyAdd()
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("AddCompany", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@CompanyName ", CompanyName);
            cmd.Parameters.AddWithValue("@ContactPerson ", CompanyContactPerson);
            cmd.Parameters.AddWithValue("@Email ", CompanyEmail);
            cmd.Parameters.AddWithValue("@Phone ", CompanyPhone);
            cmd.Parameters.AddWithValue("@Address ", CompanyAddress);
            cmd.Parameters.AddWithValue("@Password ", CompanyPassword);
            //Процедурын гаралтын параметрийг дараахь байдлаар үүсгэн холбож өгнө
            SqlParameter idParam = new SqlParameter("@CompanyId ", SqlDbType.Int);
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
            return "Компани амжилттай нэмэгдлээ";
        }
        public static string dbcon = (string)ConfigurationManager.AppSettings["dsn"];
        public static DataSet ServiceSelect()
        {
            SqlConnection con = new SqlConnection(dbcon);
            SqlDataAdapter da = new SqlDataAdapter("Select * from ServiceHistory", con);
            SqlDataAdapter db = new SqlDataAdapter("Select * from Sailor", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ServiceHistory");
            db.Fill(ds, "Sailor");
            DateTime now = DateTime.Now;
            DataTable newTable = new DataTable();
            newTable.Columns.Add("SailorId");
            newTable.Columns.Add("ContractEndDate");
            newTable.Columns.Add("SignOfDate");
            foreach (DataRow dr in ds.Tables["ServiceHistory"].Rows)
            {
                if (Convert.ToBoolean(dr["SignoffPort"])) { continue; }
                object[] oParam = new object[3];
                oParam[0] = Convert.ToInt16(dr["SailorId"]);
                if (dr["SignOfDate"].ToString() == "")
                {
                    if (Convert.ToDateTime(dr["ContractEndDate"]) <= now)
                    {
                        oParam[1] = dr["ContractEndDate"];
                        newTable.Rows.Add(oParam);
                    }
                }
                else
                {
                    if (Convert.ToDateTime(dr["SignOfDate"].ToString()) <= now)
                    {
                        oParam[2] = dr["SignOfDate"];
                        newTable.Rows.Add(oParam);
                    }
                }
            }
            ds.Tables.Add(newTable);
            return ds;
        }
        public static void getNotifications()
        {
            DataSet SailorDt = ServiceSelect();
            
        }
        public static string ServiceAdd()
        {
            ServiceContractEndDate= ServiceSignOnDate.AddMonths(ServiceContractPerion);
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("AddService", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@SailorId ", ServiceSailorId);
            cmd.Parameters.AddWithValue("@RankId ", ServiceRankId);
            cmd.Parameters.AddWithValue("@VesselId ", ServiceVesselId);
            cmd.Parameters.AddWithValue("@SignOnDate ", ServiceSignOnDate);
            cmd.Parameters.AddWithValue("@SignOnPort ", "");
            cmd.Parameters.AddWithValue("@SignOffPort ", "");
            cmd.Parameters.AddWithValue("@ContractPerion ", ServiceContractPerion);
            cmd.Parameters.AddWithValue("@ContractEndDate ", ServiceContractEndDate);
            //Процедурын гаралтын параметрийг дараахь байдлаар үүсгэн холбож өгнө
            SqlParameter idParam = new SqlParameter("@SequenceNumber ", SqlDbType.Int);
            idParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(idParam);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                IDbCommand cmd1 = conn.CreateCommand();
                cmd1.CommandText = "update sailor set JobStatus=2 where SailorId=@sailorId";
                cmd1.Parameters.Add(new SqlParameter("@sailorId", ServiceSailorId));
                cmd1.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                return ex.ToString();
            }
            finally
            {
                conn.Dispose();
            }
            return "Үйлчилгээний түүх амжилттай нэмэгдлээ";
        }

    }
}