using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeAttendance.Models.ViewModels
{
    public class AttendanceCheckInViewModel
    {
        public string AttendanceId { get; set; }
        public string StudentName { get; set; }
        public string Uri { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public int Index { get; set; }
    }
}