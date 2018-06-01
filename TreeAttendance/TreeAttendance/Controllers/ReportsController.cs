using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    public class ReportsController : Controller
    {
        /// <summary>
        /// Show list of students + class so user can pick for
        /// attendance reports
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
        public ActionResult StudentReports()
        {
            return View();
        }

        /// <summary>
        /// Generates the whole class's attendance reports
        /// </summary>
        /// <returns></returns>
        /// GET:AdminHome/ClassReports
        public ActionResult ClassReports()
        {
            return View();
        }

        public FileResult Download()
        {
            return new FilePathResult("~\\Content\\pdf\\StudentReport.pdf", "application/pdf");
        }
    }
}