using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeAttendance.Models
{
    public class StudentListModel
    {
        public List<StudentModel> StudentList { get; set; }

        public StudentListModel()
        {
            StudentList = new List<StudentModel>();
        }
    }
}