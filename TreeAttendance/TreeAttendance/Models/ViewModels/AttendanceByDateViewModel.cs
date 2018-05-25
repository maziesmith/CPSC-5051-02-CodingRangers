using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeAttendance.Models.ViewModels
{
    public class AttendanceByDateViewModel
    {
        public List<AttendanceViewModel> AttendanceList { get; set; }
        public string Date { get; set; }
    }
}