using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    /// <summary>
    /// Controller for the Admin section of the website
    /// </summary>
    public class AdminHomeController : Controller
    {
        /// <summary>
        /// Admin dashboard, displays some quick and useful information
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Reports Index
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportsIndex()
        {
            return View();
        }
        /// <summary>
        /// Student reports
        /// </summary>
        /// <returns></returns>
        public ActionResult Reports()
        {
            return View();
        }


    }
}