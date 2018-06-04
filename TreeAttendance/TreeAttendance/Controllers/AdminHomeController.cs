using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TreeAttendance.Backend;
using TreeAttendance.Models;

namespace TreeAttendance.Controllers
{
    public class AdminHomeController : Controller
    {
        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;
        private SchoolDayBackend SchoolDayBackend = SchoolDayBackend.Instance;
        private AttendanceBackend AttendanceBackend = AttendanceBackend.Instance;

        /// <summary>
        /// Admin dashboard, shows quick and helpful information
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var today = SchoolDayBackend.Index().Last();
            //Load the list of data into myAttendanceList
            var myAttendanceList = AttendanceBackend.IndexBySchoolDay(today.Id);
            var myData = new TodayAttendance(myAttendanceList, today.Id);

            return View(myData);
        }

        /// <summary>
        /// Admin Login
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
    }
}