using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JKLSite.Views.JKL
{
    public partial class ListSailor : ViewUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            IDbConnection conn = new SqlConnection(dsn);
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"Select	s.SailorId,s.SailorName,s.DateOfBirth,m.Detail MaritialStatus,
		                        s.Address,s.Height,s.Weight,s.BloodType,s.ShoeSize,j.NameMon JobStatus 
		                        from Sailor s 
		                        left join Maritial m on s.MaritialStatus=m.MaritialId
		                        left join JobStatus j on s.JobStatus=j.JobId";

            try
            {
                conn.Open();
                IDataReader reader = cmd.ExecuteReader();
                SailorGrid.DataSource = reader;
                SailorGrid.DataBind();
                reader.Close();
            }
            catch (Exception ex)
            {
                _errorMessage.Text = ex.Message;
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}