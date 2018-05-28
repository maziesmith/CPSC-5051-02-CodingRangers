using System.Web.Mvc;
using TreeAttendance.Backend;
using TreeAttendance.Models;

namespace TreeAttendance.Controllers
{
    public class StudentsController : Controller
    {
        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;

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

        /// <summary>
        /// Read information on a single Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Read(string id = null)
        {
            var myData = StudentBackend.Read(id);

            if (myData == null)
            {
                //Send to Error Page
                RedirectToAction("Error", "Home", new { id = "Invalid Record" });
            }

            return View(myData);
        }

        /// <summary>
        /// This opens up the create a new student profile page
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var myData = new StudentModel();

            return View(myData);
        }

        /// <summary>
        /// Make a new Student sent in by the create a new student profile page
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST Create
        [HttpPost]
        public ActionResult Create([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "ProfilePicutureUri,"+
                                        "")] StudentModel data)
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

            if (string.IsNullOrEmpty(data.Id))
            {
                // Return back for Edit
                return View(data);
            }

            StudentBackend.Create(data);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This will show the details of the Student to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Update
        public ActionResult Update(string id = null)
        {
            var myData = StudentBackend.Read(id);

            if (myData == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            }
            return View(myData);
        }

        /// <summary>
        /// This updates the Student based on the information posted from the update page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: Update
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "ProfilePicutureUri,"+
                                        "")] StudentModel data)
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

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for edit
                return View(data);
            }

            StudentBackend.Update(data);

            return RedirectToAction("Read", null, new {id = data.Id});
        }

        /// <summary>
        /// This shows the Student info to be deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Delete
        public ActionResult Delete(string id = null)
        {
            var myData = StudentBackend.Read(id);

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
        // POST: Delete
        [HttpPost]
        public ActionResult Delete([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "ProfilePicutureUri,"+
                                        "")] StudentModel data)
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

            StudentBackend.Delete(data.Id);

            return RedirectToAction("Index");
        }
    }
}
