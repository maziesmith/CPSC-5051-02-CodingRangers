using System.Web.Mvc;

namespace TreeAttendance.Controllers
{
    public class AdminHomeController : Controller
    {
        /// <summary>
        /// Admin dashboard, shows quick and helpful information
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
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