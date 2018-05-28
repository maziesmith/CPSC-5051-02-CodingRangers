using TreeAttendance.Backend;
using TreeAttendance.Models;
using TreeAttendance.Models.ViewModels;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;

namespace TreeAttendance.Controllers
{
    public class KioskController : Controller
    {

        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;
        private SchoolDayBackend SchoolDayBackend = SchoolDayBackend.Instance;
        private AttendanceBackend AttendanceBackend = AttendanceBackend.Instance;


        /// <summary>
        /// Return the list of students with the status of logged in or out
        /// </summary>
        /// <returns></returns>
        // GET: Kiosk
        public ActionResult Index()
        {
            var lastSchoolDay = SchoolDayBackend.Index().Last();
            var myAttendanceList = AttendanceBackend.IndexBySchoolDay(lastSchoolDay.Id);
            var myData = new AttendanceByDateViewModel();
            myData.AttendanceList = new List<AttendanceViewModel>();
            foreach (var item in myAttendanceList)
            {
                var myViewModel = new AttendanceViewModel
                {
                    Attendance = item,
                    StudentName = StudentBackend.Read(item.StudentId).Name,
                    Uri = StudentBackend.Read(item.StudentId).ProfilePictureUri
                };
                myData.AttendanceList.Add(myViewModel);
            }
            myData.Date = lastSchoolDay.Date.ToString("MM/dd/yyyy");
            return View(myData);
        }

        public ActionResult SetNewDay()
        {

            var lastSchoolDay = new SchoolDayModel(SystemGlobals.Instance.Today, SystemGlobals.Instance.DefaultExpectedHours);
            SchoolDayBackend.Create(lastSchoolDay);
            var studentList = StudentBackend.Index();
            foreach (var item in studentList)
            {
                AttendanceBackend.Create(new AttendanceModel(item.Id, lastSchoolDay.Id));
            }
            //increment today's date
            SystemGlobals.Instance.Today = SystemGlobals.Instance.Today.AddDays(1);

            return RedirectToAction("Index");
        }


        // GET: Kiosk/SetLogout/5
        public ActionResult CheckIn(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home", "Invalid Data");
            }
            //if (TimeSpan.Compare(DateTime.Now.TimeOfDay, SystemGlobals.Instance.DefaultEndTime) > 0)
            //{
            //    return RedirectToAction("Error", "Home", "Can not check in, school already ended");
            //}

            AttendanceBackend.CheckIn(id);
            return RedirectToAction("KioskTree");
        }

        // GET: Kiosk/SetLogout/5
        public ActionResult CheckOut(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home", "Invalid Data");
            }

            //if (TimeSpan.Compare(DateTime.Now.TimeOfDay, SystemGlobals.Instance.DefaultEndTime) > 0)
            //{
            //    return RedirectToAction("Error", "Home", "Can not check out, school already ended");
            //}

            AttendanceBackend.CheckOut(id);
            return RedirectToAction("Index");
        }

        //// GET: Kiosk/SetLogout/5
        //public ActionResult SetLogout(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return RedirectToAction("Error", "Home", "Invalid Data");
        //    }

        //    StudentBackend.ToggleStatusById(id);
        //    return RedirectToAction("Index");
        //}
        public ActionResult KioskTree()
        {
            return View();
        }
    }
}