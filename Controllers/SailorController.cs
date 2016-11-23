using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JKLSite.Models;
using System.Data;

namespace JKLSite.Controllers
{
    public class SailorController : Controller
    {
        // GET: Sailor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Remove("Sailor");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AboutSailor()
        {
            SailorModel sm = new SailorModel();
            DataTable dt = (DataTable) Session["Sailor"];
            object[] param,param1 = new object[dt.Rows[0].ItemArray.Length];
            param = dt.Rows[0].ItemArray;
            param1 = sm.getListName();
            DataTable ret_dt = new DataTable();
            ret_dt.Columns.Add("NAME");
            ret_dt.Columns.Add("VALUE");
            string[] tmpdrow = new string[2];
            for (int i = 0; i < param.Length - 1; i++)
            {
                tmpdrow[0] = param1[i].ToString();
                tmpdrow[1] = param[i].ToString();
                ret_dt.Rows.Add(tmpdrow);
            }
            return View(ret_dt.Rows);
        }
        public ActionResult AboutCompany()
        {
            return View();
        }
    }
}