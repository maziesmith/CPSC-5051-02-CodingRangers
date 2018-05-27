using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error(string id = "Error")
        {
            ViewBag.Message = "id";

            return View();
        }

        public ActionResult ResetApp()
        {
            Backend.StudentBackend.Instance.Reset();
            Backend.SchoolDayBackend.Instance.Reset();
            Backend.AttendanceBackend.Instance.Reset();
            return RedirectToAction("Index");
        }
    }
}