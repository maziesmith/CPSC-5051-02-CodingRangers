using System.Collections.Generic;
using System.Web.Mvc;
using TreeAttendance.Backend;
using TreeAttendance.Models;
using TreeAttendance.Models.ViewModels;

namespace TreeAttendance.Controllers
{
    // Access student page
    public class StudentHomeController : Controller
    {

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;
        private SchoolDayBackend SchoolDayBackend = SchoolDayBackend.Instance;
        private AttendanceBackend AttendanceBackend = AttendanceBackend.Instance;

        // GET: StudentHome
        // Go to student home page
        public ActionResult Index()
        {
            var myStudent = StudentBackend.Index()[0];
            return View(myStudent);
        }

        // GET: OverallPerformance
        // Access Overall Performance Page
        public ActionResult OverallPerformance()
        {
            return View();
        }


        // GET: Attendance
        // Go to attendance page
        public ActionResult Attendance(string id, int index = 0)
        {
            if (index == 0)
            {
                index = SystemGlobals.Instance.Today.Month;
            }
            //Load the list of data into myAttendanceList 
            var myAttendanceList = AttendanceBackend.IndexByStudent(id, index);

            //create view model
            var myData = new AttendanceByStudentViewModel();
            myData.AttendanceList = new List<AttendanceViewModel>();
            foreach (var item in myAttendanceList)
            {
                var myViewModel = new AttendanceViewModel
                {
                    Attendance = item,
                    Date = SchoolDayBackend.Read(item.SchoolDayId).Date,
                };
                myData.AttendanceList.Add(myViewModel);
            }
            myData.StudentName = StudentBackend.Read(id).Name;
            myData.Uri = StudentBackend.Read(id).ProfilePictureUri;

            return View(myData);
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