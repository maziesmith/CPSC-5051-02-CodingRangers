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

        // GET: Attendance/Details/5
        public ActionResult Details(string id = null)
        {
            var myData = Backend.Backend.GetAttendanceModel(id);
            return View(myData);
        }

        // GET: Attendance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attendance/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Attendance/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
