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
        public List<AttendanceModel> Attendance { get; set; }
        public List<DailyData> data { get; set; }
        public int DaysPresent { get; set; }
        public int DaysAbsentExcused { get; set; }
        public int DaysAbsentUnexcused { get; set; }
        public double HoursAttended { get; set; }
        public double HoursMissing { get; set; }
        public int DaysOnTime { get; set; }
        public int DaysLate { get; set; }
        public int DaysStayed { get; set; }
        public int DaysLeftEarly { get; set; }

        public StudentReport(List<AttendanceModel> attendance, string month, string studentName)
        {
            Month = month;
            StudentName = studentName;
            Attendance = attendance;
            data = new List<DailyData>();
            Calculate();
        }

        private void Calculate()
        {
            CalculateData();
            CalculateOther();
        }

        private void CalculateData()
        {
            Double accumulativeHoursAttended = 0;
            Double accumulativeHoursExpected = 0;
            for (int i = 0; i < Attendance.Count; i++)
            {
                AttendanceModel att = Attendance[i];
                Double hours = 0;
                Double hoursExpected = 0;
                for (int j = 0; j < att.AttendanceCheckIns.Count; j++)
                {
                    AttendanceCheckInModel checkIn = att.AttendanceCheckIns[i];
                    hours += checkIn.CheckOut.Subtract(checkIn.CheckIn).TotalHours;
                    hoursExpected += Backend.SchoolDayBackend.Instance.Read(att.SchoolDayId).ExpectedHours.TotalHours;
                }
                accumulativeHoursAttended += hours;
                accumulativeHoursExpected += hoursExpected;
                data.Add(new DailyData()
                {
                    Date = Backend.SchoolDayBackend.Instance.Read(att.SchoolDayId).Date.ToString("MM/dd"),
                    HoursAttended = hours,
                    HoursExpected = hoursExpected,
                    AccumulativeHoursAttended = accumulativeHoursAttended,
                    AccumulativeHoursExpected = accumulativeHoursExpected
                });
            }
        }

        private void CalculateOther()
        {
            foreach(var item in Attendance)
            {
                if(item.Status == Enums.AttendanceStatusEnum.AbsentExcused)
                {
                    DaysAbsentExcused++;
                } else if (item.Status == Enums.AttendanceStatusEnum.AbsentUnexcused)
                {
                    DaysAbsentUnexcused++;
                } else if(item.Status == Enums.AttendanceStatusEnum.Late || item.Status == Enums.AttendanceStatusEnum.LateLeft){
                    DaysLate++;
                } if(item.Status == Enums.AttendanceStatusEnum.OnTimeLeft || item.Status == Enums.AttendanceStatusEnum.LateLeft){
                    DaysLeftEarly++;
                }
            }
            DaysPresent = Attendance.Count - DaysAbsentExcused - DaysAbsentUnexcused;
            DaysOnTime = DaysPresent - DaysLate;
            DaysStayed = DaysPresent - DaysLeftEarly;
            HoursAttended = data.Last().AccumulativeHoursAttended;
            HoursMissing = data.Last().AccumulativeHoursExpected - data.Last().AccumulativeHoursAttended;
        }
    }
}