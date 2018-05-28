using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    public class AdminHomeController : Controller
    {
        /// <summary>
        /// Admin dashboard, shows quick and helpful information
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Generates student's attendance reports
        /// </summary>
        /// <returns></returns>
        /// GET:AdminHome/Reports
        public ActionResult Reports()
        {
            return View();
        }

        /// <summary>
        /// Show list of students + class so user can pick for
        /// attendance reports
        /// </summary>
        /// <returns></returns>
        /// GET: AdminHome/ReportsIndex/
        public ActionResult ReportsIndex()
        {
            return View();
        }
    }
}