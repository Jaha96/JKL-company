using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JKLSite.Models;
using System.Data;

namespace JKLSite.Controllers
{
    public class PartnerController : Controller
    {
        PartnerModel pm = new PartnerModel();
        // GET: Partner
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEmployee()
        {
            if (Session["Partner"] != null)
            {
                DataTable dt = (DataTable)Session["Partner"];
                int Id = Convert.ToInt32(dt.Rows[0]["CompanyId"]);
                ViewBag.Company = Id;
                ViewBag.VesselList = JKLModel.getVessel(Id);
            }
            ViewBag.RankList = JKLModel.getRank();
            return View();
        }
        public ActionResult EmployeeAdd()
        {
            pm.RankId =Convert.ToInt32(Request["RankId"]);
            pm.VesselId =Convert.ToInt32(Request["VesselId"]);
            pm.CompanyId = Convert.ToInt32(Request["CompanyId"].ToString().Split(' ')[3]);
            ViewBag.ErrorMessage = pm.EmployeeAdd();
            return View("Index");
        }
        public ActionResult VesselAdd()
        {
            return View("Index");
        }
        public ActionResult AddVessel()
        {
            if (Session["Partner"] != null)
            {
                DataTable dt = (DataTable)Session["Partner"];
                int Id = Convert.ToInt32(dt.Rows[0]["CompanyId"]);
                ViewBag.Company = Id;
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Remove("Partner");
            return RedirectToAction("Index", "Home");
        }
    }
}