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
    }
}