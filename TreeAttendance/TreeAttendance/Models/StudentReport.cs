using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeAttendance.Models
{
    public class StudentReport
    {
        public string Month { get; set; }
        public string StudentName { get; set; }
        public List<DailyData> data { get; set; }
        public int DaysPresent { get; set; }
        public int DaysAbsentExcused { get; set; }
        public int DaysAbsentUnexcused { get; set; }
        public int HoursAttended { get; set; }
        public int HoursMissing { get; set; }
        public int DaysOnTime { get; set; }
        public int DaysLate { get; set; }
        public int DaysStayed { get; set; }
        public int DaysLeftEarly { get; set; }
    }
}