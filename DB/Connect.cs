using JKLSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JKLSite.DB
{
    public class Connect
    {
        public string ServiceAdd(ServiceModel SM)
        {
            SM.EndDate = SM.StartDate.AddMonths(SM.PeriodMonth);
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("AddService", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@SailorId ", SM.SailorId);
            cmd.Parameters.AddWithValue("@RankId ", SM.RankId);
            cmd.Parameters.AddWithValue("@VesselId ", SM.VesselId);
            cmd.Parameters.AddWithValue("@SignOnDate ", SM.StartDate);
            cmd.Parameters.AddWithValue("@SignOnPort ", "");
            cmd.Parameters.AddWithValue("@SignOffPort ", "");
            cmd.Parameters.AddWithValue("@ContractPerion ", SM.PeriodMonth);
            cmd.Parameters.AddWithValue("@ContractEndDate ", SM.EndDate);
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
                cmd1.Parameters.Add(new SqlParameter("@sailorId", SM.SailorId));
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
        public string EmployeeDelete(int id)
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            SqlCommand cmd = new SqlCommand("Delete from Employee where id=@id", conn);
            cmd.Parameters.AddWithValue("@id ", id);
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
            return "1";
        }
        public DataSet SailorDS()
        {
            DataSet ds = new DataSet();
            using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
            {
                //string SQL = "select * from products where id = @ProductID";
                string SQL = @"SELECT s.SailorId,s.SailorName,s.DateOfBirth,
	                           m.Detail,s.Address,s.Height,s.Weight,
	                           s.BloodType,s.ShoeSize,j.NameMon,s.Password,s.*
	                           FROM Sailor s
	                           Left join Maritial m on s.MaritialStatus=m.MaritialId
	                           Left join JobStatus j on s.JobStatus=j.JobId";
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
            return ds;
        }
        public DataSet SailorDS(int id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
            {
                //string SQL = "select * from products where id = @ProductID";
                string SQL = @"SELECT * from Sailor where SailorId=@id";
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
            return ds;
        }
        public string SailorAdd(Sailor SM)
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("AddSailor", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@SailorName ", SM.SailorName);
            cmd.Parameters.AddWithValue("@DateOfBirth ", SM.BirthDate);
            cmd.Parameters.AddWithValue("@MaritialStatus ", SM.MaritialStatus);
            cmd.Parameters.AddWithValue("@Address ", SM.Address);
            cmd.Parameters.AddWithValue("@Height ", SM.Height);
            cmd.Parameters.AddWithValue("@Weight ", SM.Weight);
            cmd.Parameters.AddWithValue("@BloodType ", SM.BloodType);
            cmd.Parameters.AddWithValue("@ShoeSize ", SM.ShoeSize);
            cmd.Parameters.AddWithValue("@JobStatus ", SM.JobStatus);
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
        public string SailorUpdate(Sailor CM)
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("UpdateSailor", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@SailorName  ", CM.SailorName);
            cmd.Parameters.AddWithValue("@DateOfBirth  ", CM.BirthDate);
            cmd.Parameters.AddWithValue("@MaritialStatus  ", CM.MaritialStatus);
            cmd.Parameters.AddWithValue("@Address  ", CM.Address);
            cmd.Parameters.AddWithValue("@Height  ", CM.Height);
            cmd.Parameters.AddWithValue("@Weight  ", CM.Weight);
            cmd.Parameters.AddWithValue("@BloodType  ", CM.BloodType);
            cmd.Parameters.AddWithValue("@ShoeSize   ", CM.ShoeSize);
            cmd.Parameters.AddWithValue("@JobStatus  ", CM.JobStatus);
            cmd.Parameters.AddWithValue("@Password   ", CM.Password);
            cmd.Parameters.AddWithValue("@UserId ", CM.SailorId);
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
            return "Далайчин амжилттай Засагдлаа";
        }
        public string ServiceUpdate(int id,DateTime vacationDate)
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            SqlCommand cmd = new SqlCommand("update ServiceHistory set SignOfDate=@VacationDate where SequenceNumber=@id", conn);
            cmd.Parameters.AddWithValue("@VacationDate ", vacationDate);
            cmd.Parameters.AddWithValue("@id ", id);
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
            return "Статус амжилттай өөрчлөгдлөө";
        }

        public string ServiceUpdate(int id)
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            SqlCommand cmd = new SqlCommand("update ServiceHistory set SignOffPort=1 where SequenceNumber=@id", conn);
        
            cmd.Parameters.AddWithValue("@id ", id);
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
            return "Статус амжилттай өөрчлөгдлөө";
        }
        public string SailorDelete(int id)
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("DeleteSailor", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@SailorId ", id);
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
            return "Далайчин амжилттай устгагдлаа";
        }
        public DataSet CompanyDS()
        {
            DataSet ds = new DataSet();
            using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
            {
                //string SQL = "select * from products where id = @ProductID";
                string SQL = @"SELECT * from Company";
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
            return ds;
        }
        public DataSet CompanyDS(int id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["dsn"]))
            {
                //string SQL = "select * from products where id = @ProductID";
                string SQL = @"SELECT * from Company where CompanyId=@id";
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
            return ds;
        }
        public string CompanyAdd(Company CM)
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("AddCompany", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@CompanyName ", CM.CompanyName);
            cmd.Parameters.AddWithValue("@ContactPerson ", CM.ContactPerson);
            cmd.Parameters.AddWithValue("@Email ", CM.Email);
            cmd.Parameters.AddWithValue("@Phone ", CM.Phone);
            cmd.Parameters.AddWithValue("@Address ", CM.Address);
            cmd.Parameters.AddWithValue("@Password ", CM.Password);
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
        public string CompanyUpdate(Company CM)
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("UpdateCompany", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@CompanyName ", CM.CompanyName);
            cmd.Parameters.AddWithValue("@ContactPerson ", CM.ContactPerson);
            cmd.Parameters.AddWithValue("@Email ", CM.Email);
            cmd.Parameters.AddWithValue("@Phone ", CM.Phone);
            cmd.Parameters.AddWithValue("@Address ", CM.Address);
            cmd.Parameters.AddWithValue("@Password ", CM.Password);
            cmd.Parameters.AddWithValue("@CompanyId ", CM.CompanyId);
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
            return "Компани амжилттай Засагдлаа";
        }
        public string CompanyDelete(int id)
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            SqlConnection conn = new SqlConnection(dsn);
            //Коммандын төрлийг сонгох тохиолдолд дараах байдлаар үүсгэнэ.
            SqlCommand cmd = new SqlCommand("DeleteCompany", conn);
            //Коммандын төрөл профедур болохыг зааж байна
            cmd.CommandType = CommandType.StoredProcedure;
            //Уг процедурыг дуудахад дараах параметрүүдийг дамжуулна
            cmd.Parameters.AddWithValue("@CompanyId ", id);
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
            return "Компани амжилттай устгагдлаа";
        }
    }
}