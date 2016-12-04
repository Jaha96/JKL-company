using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JKLSite.Models
{
    public class SailorModel
    {
        public DataTable Sailor(int id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
            {
                //string SQL = "select * from products where id = @ProductID";
                string SQL = @"SELECT s.SailorId,s.SailorName,s.DateOfBirth,
	                           m.Detail,s.Address,s.Height,s.Weight,
	                           s.BloodType,s.ShoeSize,j.NameMon,s.Password 
	                           FROM Sailor s
	                           Left join Maritial m on s.MaritialStatus=m.MaritialId
	                           Left join JobStatus j on s.JobStatus=j.JobId	 where sailorId=@id";
                Con.Open();


                using (SqlCommand Com = new SqlCommand(SQL, Con))
                {
                    Com.Parameters.Add(new SqlParameter("@id", id));
                    using (SqlDataAdapter adap = new SqlDataAdapter(Com))
                    {
                        adap.Fill(ds);
                    }
                }
                Con.Dispose();
            }
            return ds.Tables[0];
        }
        public DataTable GetSailorWorkCompany(int id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
            {
                //string SQL = "select * from products where id = @ProductID";
                string SQL = @"select * from servicehistory sh
                                left join Vessel v on sh.VesselId=v.Vesselid
                                left join Company c on v.CompanyId=c.CompanyId
                                where SailorId=@id";
                Con.Open();


                using (SqlCommand Com = new SqlCommand(SQL, Con))
                {
                    Com.Parameters.Add(new SqlParameter("@id", id));
                    using (SqlDataAdapter adap = new SqlDataAdapter(Com))
                    {
                        adap.Fill(ds);
                    }
                }
                Con.Dispose();
            }
            return ds.Tables[0];
        }
        public object[] getListName()
        {
            object[] obj = new object[10];
            obj[0] = "Далайчны Id";
            obj[1] = "Далайчны нэр";
            obj[2] = "Төрсөн огноо";
            obj[3] = "Гэр бүлийн байдал";
            obj[4] = "Хаяг";
            obj[5] = "Өндөр";
            obj[6] = "Жин";
            obj[7] = "Цусны төрөл";
            obj[8] = "Гуталны размер";
            obj[9] = "Ажлын статус";
            return obj;
        }
    }
}