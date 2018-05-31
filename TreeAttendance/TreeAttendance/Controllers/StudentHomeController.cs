using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    // Access student page
    public class StudentHomeController : Controller
    {
        // GET: StudentHome
        public ActionResult Index()
        {
            return View();
        }

        // GET: OverallPerformance
        public ActionResult OverallPerformance()
        {
            return View();
        }


        // GET: Attendance
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
    }
}