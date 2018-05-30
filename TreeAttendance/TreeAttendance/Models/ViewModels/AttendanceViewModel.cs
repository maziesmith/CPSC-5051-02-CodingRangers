using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeAttendance.Models.ViewModels
{
    public class AttendanceViewModel
    {
        public AttendanceModel Attendance { get; set; }
        public DateTime Date { get; set; }
        public string StudentName { get; set; }
        public string Uri { get; set; }
        public int Index { get; set; }
    }
}