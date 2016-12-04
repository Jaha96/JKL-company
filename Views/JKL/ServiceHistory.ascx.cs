using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JKLSite.Views.JKL
{
    public partial class ServiceHistory : ViewUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = null;
            int sailorId = Convert.ToInt32(Session["SailorId"]);
            if (sailorId != 0)
            {
                sql = @"SELECT s.SailorName,v.Flag,sh.SignOnDate,sh.ContractPerion,c.CompanyName,r.DescMon,sh.ContractEndDate
                                 FROM ServiceHistory sh
                                 left join Sailor s on sh.SailorId=s.SailorId
                                 left join Vessel v on sh.VesselId=v.Vesselid
                                 left join Company c on v.CompanyId=c.CompanyId
                                 left join Rank r on sh.RankId=r.RankId where s.SailorId=" + sailorId.ToString();
            }
            else {
                sql = @"SELECT s.SailorName,v.Flag,sh.SignOnDate,sh.ContractPerion,c.CompanyName,r.DescMon,sh.ContractEndDate
                                 FROM ServiceHistory sh
                                 left join Sailor s on sh.SailorId=s.SailorId
                                 left join Vessel v on sh.VesselId=v.Vesselid
                                 left join Company c on v.CompanyId=c.CompanyId
                                 left join Rank r on sh.RankId=r.RankId";
            }
            string dsn = (string)ConfigurationManager.AppSettings["dsn"];
            IDbConnection conn = new SqlConnection(dsn);
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            try
            {
                conn.Open();
                IDataReader reader = cmd.ExecuteReader();
                HistoryGrid.DataSource = reader;
                HistoryGrid.DataBind();
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