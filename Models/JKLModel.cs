using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
        public static SelectListItem[] getJobStatus(int a=0)
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
                if (a == 1)
                {
                    string val = dr["JobId"].ToString();
                    if (val == "1" || val == "2") { continue; }
                }
                if (a == 2)
                {
                    string val = dr["JobId"].ToString();
                    if (val == "1" || val == "2" || val == "3") { continue; }
                }
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

        


        public static string dbcon = (string)ConfigurationManager.AppSettings["dsn"];
        public static DataSet ServiceSelect()
        {
            SqlConnection con = new SqlConnection(dbcon);
            string sqlEmployee = @"select e.*,c.CompanyName,v.VesselName,r.DescMon from Employee e
                                    left join Company c on e.CompanyId=c.CompanyId
                                    left join Vessel v on e.VesselId=v.Vesselid
                                    left join Rank r on e.RankId=r.RankId";

            string sql = @"Select * from ServiceHistory h 
                            left join Sailor s on h.SailorId = s.SailorId
                            left join Vessel v on h.VesselId=v.Vesselid
                            left join Company c on v.CompanyId=c.CompanyId";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            SqlDataAdapter daEmployee = new SqlDataAdapter(sqlEmployee, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ServiceHistory");
            daEmployee.Fill(ds, "Employee");
            DateTime now = DateTime.Now;
            DataTable newTable = new DataTable();
            newTable.Columns.Add("SailorId");
            newTable.Columns["SailorId"].DataType= System.Type.GetType("System.Int32");
            newTable.Columns.Add("HistoryId");
            newTable.Columns.Add("SailorName");
            newTable.Columns.Add("CompanyName");
            newTable.Columns.Add("ContractEndDate");
            newTable.Columns.Add("SignOfDate");
            foreach (DataRow dr in ds.Tables["ServiceHistory"].Rows)
            {
                if (Convert.ToBoolean(dr["SignoffPort"])) { continue; }
                object[] oParam = new object[6];
                oParam[0] = Convert.ToInt16(dr["SailorId"]);
                oParam[1] = dr["SequenceNumber"].ToString();
                oParam[2] = dr["SailorName"].ToString();
                oParam[3] = dr["CompanyName"].ToString();
                if (dr["SignOfDate"].ToString() == "")
                {
                    if (Convert.ToDateTime(dr["ContractEndDate"]) <= now)
                    {
                        oParam[4] = dr["ContractEndDate"];
                        newTable.Rows.Add(oParam);
                    }
                }
                else
                {
                    if (Convert.ToDateTime(dr["SignOfDate"].ToString()) <= now)
                    {
                        oParam[5] = dr["SignOfDate"];
                        newTable.Rows.Add(oParam);
                    }
                }
            }
            ds.Tables.Add(newTable);
            return ds;
        }
        public static DataSet getNotifications()
        {
            DataSet SailorDt = ServiceSelect();
            return SailorDt;
        }
        

    }
}