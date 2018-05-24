using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeAttendance.Models
{
    public class StudentListModel
    {
        /// <summary>
        /// Maintains a list of all current students.
        /// </summary>
        public List<StudentModel> StudentList { get; set; }
        
        /// <summary>
        /// Constructor, the list is empty initially.
        /// </summary>
        public StudentListModel()
        {
            StudentList = new List<StudentModel>();
        }
    }
}