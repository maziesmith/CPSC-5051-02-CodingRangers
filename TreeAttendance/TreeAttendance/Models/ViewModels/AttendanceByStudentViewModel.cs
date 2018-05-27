using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeAttendance.Models.ViewModels
{
    public class AttendanceByStudentViewModel
    {
        public List<AttendanceViewModel> AttendanceList { get; set; }
        public string StudentName { get; set; }
        public string Uri { get; set; }
    }
}