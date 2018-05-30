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
        /// Return the list of students with the check-in status, student of the week, and class forest
        /// </summary>
        /// <returns></returns>
        // GET: Kiosk
        public ActionResult Index()
        {
            //Load today's attendance list
            var lastSchoolDay = SchoolDayBackend.Index().Last();
            var myAttendanceList = AttendanceBackend.IndexBySchoolDay(lastSchoolDay.Id);

            //create view model
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

        /// <summary>
        /// Go to the next day, create a brand new kiosk check-in page, this is for demo purposes
        /// </summary>
        /// <returns></returns>
        public ActionResult SetNewDay()
        {
            //increment today's date
            SystemGlobals.Instance.Today = SystemGlobals.Instance.Today.AddDays(1);

            //create a new school day
            var lastSchoolDay = new SchoolDayModel(SystemGlobals.Instance.Today, SystemGlobals.Instance.DefaultExpectedHours);
            SchoolDayBackend.Create(lastSchoolDay);

            //load student list
            var studentList = StudentBackend.Index();
            foreach (var item in studentList)
            {
                //create attendance record for each student
                AttendanceBackend.Create(new AttendanceModel(item.Id, lastSchoolDay.Id));
            }

            return RedirectToAction("Index");
        }


        /// <summary>
        /// Check in action, check in time is set to current server time
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CheckIn(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home", "Invalid Record");
            }
            //if (TimeSpan.Compare(DateTime.Now.TimeOfDay, SystemGlobals.Instance.DefaultEndTime) > 0)
            //{
            //    return RedirectToAction("Error", "Home", "Can not check in, school already ended");
            //}

            AttendanceBackend.CheckIn(id);

            return RedirectToAction("KioskTree");
        }

        /// <summary>
        /// Check cout action, check out time is set to current server time
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Sent to this page after student check in, displays tree info
        /// </summary>
        /// <returns></returns>
        public ActionResult KioskTree()
        {
            return View();
        }
    }
}