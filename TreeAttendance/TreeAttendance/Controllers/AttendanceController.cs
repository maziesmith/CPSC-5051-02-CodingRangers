using System.Collections.Generic;
using System.Web.Mvc;
using TreeAttendance.Backend;
using TreeAttendance.Models;
using TreeAttendance.Models.ViewModels;

namespace TreeAttendance.Controllers
{
    public class AttendanceController : Controller
    {
        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;
        private SchoolDayBackend SchoolDayBackend = SchoolDayBackend.Instance;
        private AttendanceBackend AttendanceBackend = AttendanceBackend.Instance;

        /// <summary>
        /// Index, the page that shows all the Students
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Load the list of data into the myDataList
            var myDataList = StudentBackend.Index();
            return View(myDataList);
        }

        /// <summary>
        /// Index, the page that shows all the school days
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexByDate()
        {
            // Load the list of data into myDataList
            var myDataList = SchoolDayBackend.Index();
            return View(myDataList);
        }

        /// <summary>
        /// Sub-index showing all attendance records of a student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ByStudent(string id = null)
        {
            //Load the list of data into myAttendanceList 
            var myAttendanceList = AttendanceBackend.IndexByStudent(id);

            //create view model
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



        /// <summary>
        /// Sub-index showing all attendance records of a school day
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ByDate(string id = null)
        {
            //Load the list of data into myAttendanceList
            var myAttendanceList = AttendanceBackend.IndexBySchoolDay(id);

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
            myData.Date = SchoolDayBackend.Read(id).Date;

            return View(myData);
        }

        /// <summary>
        /// Read information on an attendance record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Read(string id = null)
        {
            //Load the record
            var myAttendance = AttendanceBackend.Read(id);

            if (myAttendance == null)
            {
                //Send to home error page
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            //create view model
            var myData = new AttendanceViewModel
            {
                Attendance = myAttendance,
                Date = SchoolDayBackend.Read(myAttendance.SchoolDayId).Date.ToString("MM/dd/yyyy"),
                StudentName = StudentBackend.Read(myAttendance.StudentId).Name,
                Uri = StudentBackend.Read(myAttendance.StudentId).ProfilePictureUri
            };

            return View(myData);
        }

        /// <summary>
        /// This opens up the create a new check-in page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: CreateCheckIn
        public ActionResult CreateCheckIn(string id = null)
        {
            //Load the record
            var myAttendance = AttendanceBackend.Read(id);

            if (myAttendance == null)
            {
                //Send to home error page
                RedirectToAction("Error", "Home", "Invalid Record");
            }

            //create view model
            var myData = new AttendanceCheckInViewModel();
            myData.AttendanceId = myAttendance.Id;
            myData.CheckIn = SystemGlobals.Instance.DefaultStartTime;
            myData.CheckOut = SystemGlobals.Instance.DefaultEndTime;
            myData.Date = SchoolDayBackend.Read(myAttendance.SchoolDayId).Date.ToString("MM/dd/yyyy");
            myData.StudentName = StudentBackend.Read(myAttendance.StudentId).Name;
            myData.Uri = StudentBackend.Read(myAttendance.StudentId).ProfilePictureUri;

            return View(myData);
        }

        /// <summary>
        /// Make a new check-in record sent in by the create a new check-in page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: CreateCheckIn
        [HttpPost]
        public ActionResult CreateCheckIn([Bind(Include=
                                        "AttendanceId,"+
                                        "CheckIn,"+
                                        "CheckOut,"+
                                        "Index,"+
                                        "Date,"+
                                        "StudentName,"+
                                        "Uri,"+
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

            AttendanceBackend.CreateCheckIn(data);

            return RedirectToAction("Read", null, new { id = data.AttendanceId });
        }

        /// <summary>
        /// This will show the details of the check-in to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UpdateCheckIn
        public ActionResult UpdateCheckIn(string id = null, int index = 0)
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
            myData.Date = SchoolDayBackend.Read(myAttendance.SchoolDayId).Date.ToString("MM/dd/yyyy");
            myData.StudentName = StudentBackend.Read(myAttendance.StudentId).Name;
            myData.Uri = StudentBackend.Read(myAttendance.StudentId).ProfilePictureUri;
            return View(myData);
        }

        /// <summary>
        /// This updates the check-in based on the information posted from the update check-in page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: UpdateCheckIn
        [HttpPost]
        public ActionResult UpdateCheckIn([Bind(Include=
                                        "AttendanceId,"+
                                        "CheckIn,"+
                                        "CheckOut,"+
                                        "Index,"+
                                        "Date,"+
                                        "StudentName,"+
                                        "Uri,"+
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

            AttendanceBackend.UpdateCheckIn(data);

            return RedirectToAction("Read", null, new{id = data.AttendanceId } );
        }

        /// <summary>
        /// This will show the details of the check-in to delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: DeleteCheckIn
        public ActionResult DeleteCheckIn(string id = null, int index = 0)
        {
            var myAttendance = AttendanceBackend.Read(id);
            if (myAttendance == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }
            var myData = new AttendanceCheckInViewModel();
            myData.AttendanceId = myAttendance.Id;
            myData.CheckIn = myAttendance.AttendanceCheckIns[index].CheckIn;
            myData.Index = index;
            myData.Date = SchoolDayBackend.Read(myAttendance.SchoolDayId).Date.ToString("MM/dd/yyyy");
            myData.StudentName = StudentBackend.Read(myAttendance.StudentId).Name;
            myData.Uri = StudentBackend.Read(myAttendance.StudentId).ProfilePictureUri;
            return View(myData);
        }

        /// <summary>
        /// This delets the check-in based on the information posted from the delete check-in page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: DeleteCheckIn
        [HttpPost]
        public ActionResult DeleteCheckIn([Bind(Include=
                                        "AttendanceId,"+
                                        "CheckIn,"+
                                        "CheckOut,"+
                                        "Index,"+
                                        "Date,"+
                                        "StudentName,"+
                                        "Uri,"+
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

            AttendanceBackend.DeleteCheckIn(data.AttendanceId, data.Index);

            return RedirectToAction("Read", null, new { id = data.AttendanceId });
        }

        /// <summary>
        /// This shows the attendance record info to be deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Delete
        public ActionResult Delete(string id = null)
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

            return View(myData);
        }

        /// <summary>
        /// This deletes the Attendance record sent up as a post from the attendance delete page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Delete
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

