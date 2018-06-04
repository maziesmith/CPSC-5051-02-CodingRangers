﻿using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    // Access student page
    public class StudentHomeController : Controller
    {
        // GET: StudentHome
        // Go to student home page
        public ActionResult Index()
        {
            return View();
        }

        // GET: OverallPerformance
        // Access Overall Performance Page
        public ActionResult OverallPerformance()
        {
            return View();
        }


        // GET: Attendance
        // Go to attendance page
        public ActionResult Attendance()
        {
            return View();
        }

        /// <summary>
        /// Student Login
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// Student Decoration Store
        public ActionResult Store()
        {
            return View();
        }

        /// Rules Page
        public ActionResult Rules()
        {
            return View();
        }
    }
}