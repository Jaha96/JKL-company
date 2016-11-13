using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JKLSite.Models;

namespace JKLSite.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin()
        {
            Login lg = new Login();
            lg.Name = Request["name"];
            lg.Password = Request["password"];
            lg.UserType =Convert.ToInt16( Request["rd1"]);
            
            switch(lg.UserType)
            {
                case 1:
                    if (lg.Connect()) {
                        Session["User"] = lg.Name;
                        return RedirectToAction("Index", "JKL");
                    }
                    else {
                        ViewBag.Message = "Нууц үг эсвэл нэвтрэх нэр буруу байна!";
                        return View("Index", ViewBag);
                    }
                case 2:
                    break;
            }

            return View("Index");
        }
    }
}