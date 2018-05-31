using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Landing page showing key features
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Error page displaying hint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Error(string id = "Error")
        {
            ViewBag.Message = id;

            return View();
        }

        /// <summary>
        /// For demo purposes, reset data to original seeds.
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetApp()
        {
            Models.SystemGlobals.Instance.Today = DateTime.Now.AddHours(-7);
            Backend.StudentBackend.Instance.Reset();
            Backend.SchoolDayBackend.Instance.Reset();
            Backend.AttendanceBackend.Instance.Reset();

            return RedirectToAction("Index");
        }
    }
}