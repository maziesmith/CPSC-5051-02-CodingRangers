using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    public class StudentHomeController : Controller
    {
        // GET: StudentHome
        public ActionResult Index()
        {
            return View();
        }
    }
}