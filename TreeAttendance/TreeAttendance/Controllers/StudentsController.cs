using System.Web.Mvc;
using TreeAttendance.Backend;

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

        // GET: Students/Create
        public ActionResult Create()
        {
            var myData = new Models.StudentModel();
            myData.ProfilePictureUri = "boy1.png";
            return View(myData);
        }

        // POST: Students/Create
        [HttpPost]
        public ActionResult Create([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "ProfilePictureUri,"+
                                        "")] Models.StudentModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return View(data);
            }

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

            Backend.Backend.CreateStudent(data);

            return RedirectToAction("Index");
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id = null)
        {
            var myData = Backend.Backend.GetStudentModel(id);
            return View(myData);
        }

        // POST: Students/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "ProfilePictureUri,"+
                                        "")] Models.StudentModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return View(data);
            }

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

            Backend.Backend.EditStudent(data);

            return RedirectToAction("Index");
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id = null)
        {
            var myData = Backend.Backend.GetStudentModel(id);
            return View(myData);
        }

        // POST: Students/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include=
                                        "Id,"+
                                        "Name,"+
                                        "ProfilePictureUri,"+
                                        "")] Models.StudentModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit, with Error Message
                return View(data);
            }

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

            Backend.Backend.DeleteStudent(data);

            return RedirectToAction("Index");
        }
    }
}
