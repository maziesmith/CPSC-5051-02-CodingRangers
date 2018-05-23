using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            var myData = Backend.Backend.StudentListBackend.StudentList;
            return View(myData);
        }

        // GET: Attendance
        public ActionResult ByStudent(string id = null)
        {
            var myData = Backend.Backend.GetStudentModel(id);
            return View(myData);
        }

        // GET: Attendance/IndexByDate
        public ActionResult IndexByDate()
        {
            var myData = Backend.Backend.SchoolDayListBackend.SchoolDayList;
            return View(myData);
        }

        // GET: Attendance
        public ActionResult ByDate(string id = null)
        {
            var myData = Backend.Backend.GetSchoolDayModel(id);
            return View(myData);
        }

        // GET: Attendance/Details/5
        public ActionResult Details(string id = null)
        {
            var myData = Backend.Backend.GetAttendanceModel(id);
            return View(myData);
        }

        //// GET: Attendance/Create
        //public ActionResult Create(string id = null)
        //{
        //    var myData = new Models.ViewModels.CreateAttendanceViewModel();
        //    myData.SchoolDay = Backend.Backend.GetSchoolDayModel(id);
        //    myData.StudentList = Backend.Backend.StudentListBackend.StudentList;
        //    return View(myData);
        //}

        //// POST: Attendance/Create
        //[HttpPost]
        //public ActionResult Create([Bind(Include=
        //                                "SelectedStudentId,"+
        //                                "SchoolDay,"+
        //                                "StudentList,"+
        //                                "")] Models.ViewModels.CreateAttendanceViewModel data)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        // Send back for edit, with Error Message
        //        return View(data);
        //    }

        //    if (data == null)
        //    {
        //        // Send to Error Page
        //        return RedirectToAction("Error", new { route = "Home", action = "Error" });
        //    }
        //    Models.StudentModel stu = Backend.Backend.GetStudentModel(data.SelectedStudentId);
        //    Models.AttendanceModel att = new Models.AttendanceModel(stu, data.SchoolDay);
        //    Backend.Backend.CreateAttendance(att);
        //    return RedirectToAction("Index");
        //}

        // GET: Attendance/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Attendance/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Attendance/Delete/5
        public ActionResult Delete(string id = null)
        {
            var myData = Backend.Backend.GetAttendanceModel(id);
            return View(myData);
        }

        // POST: Attendance/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include=
                                        "Id,"+
                                        "SchoolDay,"+
                                        "Student,"+
                                        "ExcusedAbsence,"+
                                        "")] Models.AttendanceModel data)
        {
            //if (!ModelState.IsValid)
            //{
            //    // Send back for edit, with Error Message
            //    return View(data);
            //}

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", new { route = "Home", action = "Error" });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Sind back for Edit
                return View(data);
            }

            Backend.Backend.DeleteAttendance(data);

            return RedirectToAction("Index");
        }
    }
}
