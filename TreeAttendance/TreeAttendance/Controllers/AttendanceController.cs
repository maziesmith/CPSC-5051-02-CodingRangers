using System.Web.Mvc;
using TreeAttendance.Backend;
using TreeAttendance.Models;
using TreeAttendance.Models.ViewModels;
using System.Collections.Generic;

namespace TreeAttendance.Controllers
{
    public class AttendanceController : Controller
    {
        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;
        private SchoolDayBackend SchoolDayBackend = SchoolDayBackend.Instance;
        private AttendanceBackend AttendanceBackend = AttendanceBackend.Instance;

        // GET: Student
        /// <summary>
        /// Index, the page that shows all the Students
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Load the list of data into the StudentList
            var myDataList = StudentBackend.Index();
            return View(myDataList);
        }

        // GET: Student
        /// <summary>
        /// Index, the page that shows all the Students
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexByDate()
        {
            // Load the list of data into the StudentList
            var myDataList = SchoolDayBackend.Index();
            return View(myDataList);
        }

        // GET: Attendance
        public ActionResult ByStudent(string id = null)
        {
            var myAttendanceList = AttendanceBackend.IndexByStudent(id);
            var myData = new AttendanceByStudentViewModel();
            myData.AttendanceList = new List<AttendanceViewModel>();
            foreach (var item in myAttendanceList)
            {
                var myViewModel = new AttendanceViewModel
                {
                    Attendance = item,
                    Date = SchoolDayBackend.Read(item.SchoolDayId).Date.ToString("MM/dd/yyyy"),
                };
                myData.AttendanceList.Add(myViewModel);
            }
            myData.StudentName = StudentBackend.Read(id).Name;
            myData.Uri = StudentBackend.Read(id).ProfilePictureUri;
            return View(myData);
        }



        // GET: Attendance
        public ActionResult ByDate(string id = null)
        {
            var myAttendanceList = AttendanceBackend.IndexBySchoolDay(id);
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
            myData.Date = SchoolDayBackend.Read(id).Date.ToString("MM/dd/yyyy");
            return View(myData);
        }

        /// <summary>
        /// Read information on a single Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Student/Details/5
        public ActionResult Read(string id = null)
        {
            var myAttendance = AttendanceBackend.Read(id);
            if (myAttendance == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }
            var myData = new AttendanceViewModel
            {
                Attendance = myAttendance,
                Date = SchoolDayBackend.Read(myAttendance.SchoolDayId).Date.ToString("MM/dd/yyyy"),
                StudentName = StudentBackend.Read(myAttendance.StudentId).Name,
                Uri = StudentBackend.Read(myAttendance.StudentId).ProfilePictureUri
            };
            if (myData == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            return View(myData);
        }

        /// <summary>
        /// This will show the details of the Student to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Student/Edit/5
        public ActionResult Update(string id = null, int index = 0)
        {
            var myAttendance = AttendanceBackend.Read(id);
            if (myAttendance == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }
            var myData = new AttendanceCheckInViewModel();
            myData.AttendanceId = myAttendance.Id;
            myData.CheckIn = myAttendance.AttendanceCheckIns[index].CheckIn;
            myData.CheckOut = myAttendance.AttendanceCheckIns[index].CheckOut;
            myData.Index = index;

            return View(myData);
        }

        /// <summary>
        /// This updates the Student based on the information posted from the udpate page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Student/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "AttendanceId,"+
                                        "CheckIn,"+
                                        "CheckOut,"+
                                        "Index,"+
                                        "")] AttendanceCheckInViewModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.AttendanceId))
            {
                // Send back for edit
                return View(data);
            }

            AttendanceBackend.Update(data);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This shows the Student info to be deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Student/Delete/5
        public ActionResult Delete(string id = null)
        {
            var myData = AttendanceBackend.Read(id);

            if (myData == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            return View(myData);
        }

        /// <summary>
        /// This deletes the Student sent up as a post from the Student delete page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "ProfilePicutureUri,"+
                                        "")] AttendanceModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }
            if (data == null)
            {
                // Send to Error page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for Edit
                return View(data);
            }

            AttendanceBackend.Delete(data.Id);

            return RedirectToAction("Index");
        }
    }
}

