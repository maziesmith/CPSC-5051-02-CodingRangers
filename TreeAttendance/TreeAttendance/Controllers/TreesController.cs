using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeAttendance.Backend;
using TreeAttendance.Models;

namespace TreeAttendance.Controllers
{

    public class TreesController : Controller
    {
        // The Backend Data source
        private StudentBackend StudentBackend = StudentBackend.Instance;

        // GET: Trees
        /// <summary>
        /// This page presents all students' trees
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // Load the list of data into the myDataList
            var myDataList = StudentBackend.Index();
            return View(myDataList);
        }

        // GET: Trees/Details/5
        /// <summary>
        ///ToDo: Detail information of each tree
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            return View();
        }

        // GET: Trees/Edit/5
        /// <summary>
        /// ToDo: Admin can edit tree stages and leaves count here
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id = null)
        {
            if(id == null)
            {
                RedirectToAction("Error", "Home", "Invalid Record");
            } else if (id == "gold") {
                SystemGlobals.Instance.LeafCount += 3;
            }
            else if (id == "silver")
            {
                SystemGlobals.Instance.LeafCount += 2;
            }
            else if (id == "bronze")
            {
                SystemGlobals.Instance.LeafCount += 1;
            }
            return RedirectToAction("Details");
        }


    }
}
