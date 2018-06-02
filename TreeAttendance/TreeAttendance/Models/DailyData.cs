using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeAttendance.Models
{
    public class DailyData
    {
        public string Date { get; set; }
        public double HoursAttended { get; set; }
        public double HoursExpected { get; set; }
        public double AccumulativeHoursAttended { get; set; }
        public double AccumulativeHoursExpected { get; set; }
    }
}