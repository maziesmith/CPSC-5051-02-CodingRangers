using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeAttendance.Backend;
using TreeAttendance.Models;
namespace TreeAttendance.Controllers
{
    public class ReportsController : Controller
    {

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;
        private SchoolDayBackend SchoolDayBackend = SchoolDayBackend.Instance;
        private AttendanceBackend AttendanceBackend = AttendanceBackend.Instance;

        /// <summary>
        /// Show list of students + class so user can pick for
        /// attendance reports
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Load the list of data into the myDataList
            var myDataList = StudentBackend.Index();
            return View(myDataList);
        }

        /// <summary>
        /// Generates student's attendance reports
        /// </summary>
        /// <returns></returns>
        /// GET:AdminHome/Reports
        public ActionResult StudentReports(string id, int index = 0)
        {
            if (index == 0)
            {
                index = SystemGlobals.Instance.Today.Month;
            }
            //Load the list of data into myAttendanceList 
            var myAttendanceList = AttendanceBackend.IndexByStudent(id, index);
            var report = new StudentReport(myAttendanceList);
            return View(report);
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