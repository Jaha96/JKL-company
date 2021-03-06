﻿using System;
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
    public partial class ListCompany : ViewUserControl
    {
        string dbcon = (string)ConfigurationManager.AppSettings["dsn"];
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        int Companyid=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                BindData();
                
            }
        }
        public void BindData()
        {
            Companyid = Convert.ToInt32(Session["CompanyId"]);
            string sql;
            if (Companyid == 0) { sql = "Select * from Company"; }
            else { sql = "Select * from Company where CompanyId=" + Companyid.ToString(); }
            con = new SqlConnection(dbcon);
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            da.Fill(ds, "Company");
            CompanyGrid.DataSource = ds.Tables["Company"].DefaultView;
            CompanyGrid.DataBind();
        }

        protected void CompanyGrid_DeleteCommand1(object source, DataGridCommandEventArgs e)
        {
            con = new SqlConnection(dbcon);
            cmd = new SqlCommand("Delete from Company where CompanyId=" + CompanyGrid.DataKeys[e.Item.ItemIndex].ToString(), con);
            con.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Deleted", "<script>alert('Амжилттай устгагдлаа!')</script>");
                con.Close();
                BindData();
            }
        }
    }
    }