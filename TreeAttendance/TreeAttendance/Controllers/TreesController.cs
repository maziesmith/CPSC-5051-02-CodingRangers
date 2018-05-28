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
        // GET: Trees
        /// <summary>
        /// This page presents all students' trees
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {      
            return View();          
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
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Trees/Edit/5
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

    }
}
