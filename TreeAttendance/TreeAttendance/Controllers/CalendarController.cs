using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        /// <summary>
        /// This page presents a calendar containing 
        /// class schedule
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

       
    }
}
