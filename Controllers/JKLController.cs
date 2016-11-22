using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JKLSite.Models;

namespace JKLSite.Controllers
{
    public class JKLController : Controller
    {
        // GET: JKL
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterSailor()
        {
            ViewBag.MaritialList = JKLModel.getMaritialStatus();
            ViewBag.BloodType = JKLModel.getBloodType();
            ViewBag.JobStatus= JKLModel.getJobStatus();
            return View();
        }
        public ActionResult RegisterCompany()
        {
            return View();
        }
        public ActionResult ListSailor()
        {
            return View("Sailor");
        }
        public ActionResult Notifications()
        {
            JKLModel.getNotifications();
            return View();
        }
        public ActionResult ListCompany()
        {
            return View("Company");
        }
        [HttpPost]
        public ActionResult SailorAdd()
        {
            JKLModel.sailorName = Request["sailorName"];
            JKLModel.dateOfBirth =Convert.ToDateTime( Request["birthDate"]);
            JKLModel.maritialStatus = Convert.ToInt16(Request["maritial"]);
            JKLModel.address = Request["address"];
            JKLModel.height = Convert.ToDouble(Request["height"]);
            JKLModel.weight = Convert.ToDouble(Request["weight"]);
            JKLModel.bloodType = Request["bloodType"];
            JKLModel.shoeSize = Convert.ToInt16(Request["shoeSize"]);
            JKLModel.jobStatus = Convert.ToInt16(Request["JobStatus"]);
            ViewBag.ErrorMessage= JKLModel.SailorAdd();
            return View("Index");
        }
        public ActionResult CompanyAdd()
        {
            JKLModel.CompanyName = Request["CompanyName"];
            JKLModel.CompanyContactPerson = Request["ContactPerson"];
            JKLModel.CompanyEmail = Request["Email"];
            JKLModel.CompanyPhone = Request["Phone"];
            JKLModel.CompanyAddress = Request["Address"];
            JKLModel.CompanyPassword = Request["Password"];
            ViewBag.ErrorMessage = JKLModel.CompanyAdd();
            return View("Index");
        }
        public ActionResult RegisterService()
        {
            ViewBag.SailorList= JKLModel.getSailor();
            ViewBag.RankList= JKLModel.getRank();
            ViewBag.VesselList= JKLModel.getVessel(null);
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ServiceAdd()
        {
            JKLModel.ServiceSailorId=Convert.ToInt16( Request["SailorId"]);
            JKLModel.ServiceRankId = Convert.ToInt16(Request["RankId"]);
            JKLModel.ServiceVesselId = Convert.ToInt16(Request["VesselId"]);
            JKLModel.ServiceSignOnDate = Convert.ToDateTime(Request["SignOnDate"]);
            JKLModel.ServiceContractPerion = Convert.ToInt16(Request["ContractPerion"]);
            ViewBag.ErrorMessage = JKLModel.ServiceAdd();
            return View("Index");
        }
    }
}