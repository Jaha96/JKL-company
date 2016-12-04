using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JKLSite.Models;
using JKLSite.DB;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace JKLSite.Controllers
{
    public class JKLController : Controller
    {
        // GET: JKL
        Connect cn = new Connect();
        Company cm = new Company();
        Sailor sm = new Sailor();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterSailor()    // Calling when we first hit controller.
        {
            ViewBag.MaritialList = JKLModel.getMaritialStatus();
            ViewBag.BloodTypeList = JKLModel.getBloodType();
            ViewBag.JobStatusList= JKLModel.getJobStatus();
            return View();
        }
        [HttpPost]
        public ActionResult RegisterSailor(Sailor SM)
        {
            ViewBag.ErrorMessage = cn.SailorAdd(SM);
            ViewBag.MaritialList = JKLModel.getMaritialStatus();
            ViewBag.BloodTypeList = JKLModel.getBloodType();
            ViewBag.JobStatusList = JKLModel.getJobStatus();
            return View();
        }
        public ActionResult EditSailor(int id)
        {
            DataSet ds = cn.SailorDS(id);
            DataRow dr = ds.Tables[0].Rows[0];
            sm.SailorId = Convert.ToInt32(dr["SailorId"]);
            sm.SailorName = dr["SailorName"].ToString();
            sm.BirthDate = Convert.ToDateTime(dr["DateOfBirth"]);
            sm.MaritialStatus = Convert.ToInt32(dr["MaritialStatus"]);
            sm.Address = dr["Address"].ToString();
            sm.Height = Convert.ToDouble(dr["Height"]);
            sm.Weight = Convert.ToDouble(dr["Weight"]);
            sm.BloodType = dr["BloodType"].ToString();
            sm.ShoeSize = Convert.ToInt32(dr["ShoeSize"]);
            sm.JobStatus = Convert.ToInt32(dr["JobStatus"]);
            sm.Password = dr["Password"].ToString();
            sm.Type = 2.ToString();//edit Type
            ViewBag.MaritialList = JKLModel.getMaritialStatus();
            ViewBag.BloodTypeList = JKLModel.getBloodType();
            ViewBag.JobStatusList = JKLModel.getJobStatus();
            return View("RegisterSailor", sm);
        }

        public ActionResult StatusSailor(int id)
        {
            int notifType = Convert.ToInt32(Request["type"]);
            int historyId = Convert.ToInt32(Request["historyId"]);
            DataSet ds = cn.SailorDS(id);
            DataRow dr = ds.Tables[0].Rows[0];
            sm.SailorId = Convert.ToInt32(dr["SailorId"]);
            sm.SailorName = dr["SailorName"].ToString();
            sm.BirthDate = Convert.ToDateTime(dr["DateOfBirth"]);
            sm.MaritialStatus = Convert.ToInt32(dr["MaritialStatus"]);
            sm.Address = dr["Address"].ToString();
            sm.Height = Convert.ToDouble(dr["Height"]);
            sm.Weight = Convert.ToDouble(dr["Weight"]);
            sm.BloodType = dr["BloodType"].ToString();
            sm.ShoeSize = Convert.ToInt32(dr["ShoeSize"]);
            sm.JobStatus = Convert.ToInt32(dr["JobStatus"]);
            sm.Password = dr["Password"].ToString();
            sm.Type = 3.ToString();//Type:status change 
            ViewBag.MaritialList = JKLModel.getMaritialStatus();
            ViewBag.BloodTypeList = JKLModel.getBloodType();
            ViewBag.notificationType = notifType;
            ViewBag.HistoryId1 = historyId;
            if (notifType == 1) { ViewBag.JobStatusList = JKLModel.getJobStatus(1); }
            if (notifType == 2) { ViewBag.JobStatusList = JKLModel.getJobStatus(2); }

            return View("StatusSailor", sm);
        }
        [HttpPost]
        public ActionResult StatusSailor(Sailor SM)
        {
            int historyId = Convert.ToInt32(Request["historyId"]);
            ViewBag.ErrorMessage = cn.SailorUpdate(SM)+"\n";
            if (SM.JobStatus == 3)
            {
                DateTime now = DateTime.Now;
                DateTime vacationDate = now.AddMonths(SM.VacationMonth);
                ViewBag.ErrorMessage += cn.ServiceUpdate(historyId, vacationDate);
            }
            else
            {
                ViewBag.ErrorMessage += cn.ServiceUpdate(historyId);
            }
            return RedirectToAction("Notifications");
        }
        [HttpPost]
        public ActionResult EditSailor(Sailor SM)
        {
            ViewBag.ErrorMessage = cn.SailorUpdate(SM);
            return View("Sailor");
        }
        public ActionResult DeleteSailor(int id)
        {
            ViewBag.ErrorMessage = cn.SailorDelete(id);
            return RedirectToAction("ListSailor");
        }
        public ActionResult RegisterCompany()   // Calling when we first hit controller.
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCompany(Company CM) // Calling on http post (on Submit)
        {
            ViewBag.ErrorMessage = cn.CompanyAdd(CM);
            return View();
        }
        public ActionResult EditCompany(int id) 
        {
            DataSet ds= cn.CompanyDS(id);
            DataRow dr = ds.Tables[0].Rows[0];
            cm.CompanyId = id;
            cm.CompanyName = dr.ItemArray[1].ToString();
            cm.ContactPerson = dr.ItemArray[2].ToString();
            cm.Email= dr.ItemArray[3].ToString();
            cm.Phone= dr.ItemArray[4].ToString();
            cm.Address= dr.ItemArray[5].ToString();
            cm.Password= dr.ItemArray[6].ToString();
            cm.Type = 2.ToString();//edit Type
            return View("RegisterCompany",cm);
        }
        [HttpPost]
        public ActionResult EditCompany(Company CM)
        {
            ViewBag.ErrorMessage = cn.CompanyUpdate(CM);
            return View("RegisterCompany");
        }
        public ActionResult DeleteCompany(int id)
        {
            ViewBag.ErrorMessage = cn.CompanyDelete(id);
            return RedirectToAction("ListCompany");
        }
       
        public ActionResult ListSailor()
        {
            return View("Sailor",cn.SailorDS().Tables[0].Rows);
        }
        public ActionResult Notifications()
        {
            return View(JKLModel.getNotifications());
        }
        public ActionResult ListCompany()
        {
            return View("Company",cn.CompanyDS().Tables[0].Rows);
        }
        public ActionResult RegisterService()
        {
            ViewBag.SailorList= JKLModel.getSailor();
            ViewBag.RankList= JKLModel.getRank();
            ViewBag.VesselList= JKLModel.getVessel(null);
            return View();
        }
        [HttpPost]
        public ActionResult RegisterService(ServiceModel SM) // Calling on http post (on Submit)
        {
            ViewBag.ErrorMessage = cn.ServiceAdd(SM);
            return View();
        }
        public ActionResult RegisterEmployee()
        {
            int empId = Convert.ToInt32(Request["empId"]);
            int companyId = Convert.ToInt32(Request["companyId"]);
            int vesselId = Convert.ToInt32(Request["vesselId"]);
            int rankId = Convert.ToInt32(Request["rankId"]);
            ServiceModel SM = new ServiceModel();
            SM.HistoryId = empId;
            SM.VesselId = vesselId;
            SM.RankId = rankId;
            ViewBag.SailorList = JKLModel.getSailor();
            ViewBag.RankList = JKLModel.getRank();
            ViewBag.VesselList = JKLModel.getVessel(null);
            return View("AddEmployee",SM);
        }
        [HttpPost]
        public ActionResult RegisterEmployee(ServiceModel SM) // Calling on http post (on Submit)
        {
            ViewBag.SailorList = JKLModel.getSailor();
            ViewBag.RankList = JKLModel.getRank();
            ViewBag.VesselList = JKLModel.getVessel(null);
            ViewBag.ErrorMessage = cn.ServiceAdd(SM);
            cn.EmployeeDelete(SM.HistoryId);
            return View("AddEmployee");
        }
        public ActionResult LogOut()
        {
            Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }
    }
}