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
        public List<string> Date { get; set; }
        public List<double> HoursExpected { get; set; }
        public List<double> HoursAttended { get; set; }
        public List<double> AccumulativeHoursExpected { get; set; }
        public List<double> AccumulativeHoursAttended { get; set; }
        public int DaysPresent { get; set; }
        public int DaysAbsentExcused { get; set; }
        public int DaysAbsentUnexcused { get; set; }
        public double TotalHoursAttended { get; set; }
        public double TotalHoursMissing { get; set; }
        public int DaysOnTime { get; set; }
        public int DaysLate { get; set; }
        public int DaysStayed { get; set; }
        public int DaysLeftEarly { get; set; }

        public StudentReport(List<AttendanceModel> attendance, string month, string studentName)
        {
            Month = month;
            StudentName = studentName;
            Attendance = attendance;
            Date = new List<string>();
            HoursExpected = new List<double>();
            HoursAttended = new List<double>();
            AccumulativeHoursExpected = new List<double>();
            AccumulativeHoursAttended = new List<double>();
            Calculate();
        }

        private void Calculate()
        {
            CalculateData();
            CalculateOther();
        }

        private void CalculateData()
        {
            double accumulativeHoursAttended = 0;
            double accumulativeHoursExpected = 0;
            for (int i = 0; i < Attendance.Count; i++)
            {
                AttendanceModel att = Attendance[i];
                double hours = 0;
                double hoursExpected = 0;
                for (int j = 0; j < att.AttendanceCheckIns.Count; j++)
                {
                    AttendanceCheckInModel checkIn = att.AttendanceCheckIns[j];
                    hours += checkIn.CheckOut.Subtract(checkIn.CheckIn).TotalHours;

                }
                hoursExpected = Backend.SchoolDayBackend.Instance.Read(att.SchoolDayId).ExpectedHours.TotalHours;
                accumulativeHoursAttended += hours;
                accumulativeHoursExpected += hoursExpected;
                Date.Add(Backend.SchoolDayBackend.Instance.Read(att.SchoolDayId).Date.ToString("dd"));
                HoursAttended.Add(hours);
                HoursExpected.Add(hoursExpected);
                AccumulativeHoursAttended.Add(accumulativeHoursAttended);
                AccumulativeHoursExpected.Add(accumulativeHoursExpected);
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
            TotalHoursAttended = AccumulativeHoursAttended.Last();
            TotalHoursMissing = AccumulativeHoursExpected.Last() - AccumulativeHoursAttended.Last();
        }
    }
}