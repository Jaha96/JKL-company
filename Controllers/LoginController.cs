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
            
            if(!lg.Connect())
            {
                ViewBag.Message = "Нууц үг эсвэл нэвтрэх нэр буруу байна!";
                return View("Index", ViewBag);
            }
            else
            { 
                switch(lg.UserType)
                {
                    case 1:
                            Session["User"] = lg.Name;
                            return RedirectToAction("Index", "JKL");
                   
                    case 2:
                            Session["Partner"] = lg.getCompany();
                            return RedirectToAction("Index", "Partner");
                    case 3:
                        break;
                }
            }

            return View("Index");
        }
    }
}