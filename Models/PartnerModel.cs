using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JKLSite.Models
{
    public class PartnerModel
    {
        public string VesselName { get; set; }
        public int CompanyId { get; set; }
        public int VesselId { get; set; }
        public int RankId { get; set; }
        public string VesselType { get; set; }
        public string Flag { get; set; }
        public string VesselAdd()
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("AddVessel", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@VesselName", VesselName);
            cmd.Parameters.AddWithValue("@VesselType", VesselType);
            cmd.Parameters.AddWithValue("@Flag", Flag);
            //Процедурын гаралтын параметрийг дараахь байдлаар үүсгэн холбож өгнө
            SqlParameter idParam = new SqlParameter("@Vesselid  ", SqlDbType.Int);
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
            return "Усан онгоц амжилттай нэмэгдлээ";
        }
        public string EmployeeAdd()
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("AddEmployee", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@VesselId", VesselId);
            cmd.Parameters.AddWithValue("@RankId", RankId);
            //Процедурын гаралтын параметрийг дараахь байдлаар үүсгэн холбож өгнө
            SqlParameter idParam = new SqlParameter("@Id ", SqlDbType.Int);
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
            return "Орон тоо амжилттай нэмэгдлээ";
        }
        
    }
}