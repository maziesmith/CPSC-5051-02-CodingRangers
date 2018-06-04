using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeAttendance.Models
{
    public class TodayAttendance
    {
        public List<AttendanceModel> Attendance { get; set; }
        public int Current { get; set; }
        public int Present { get; set; }
        public int OnTime { get; set; }
        public int Left { get; set; }
        public string TodayId { get; set; }
        public TodayAttendance(List<AttendanceModel> attendance, string todayId)
        {
            Attendance = attendance;
            TodayId = todayId;
            Compute();
        }

        public void Compute()
        {
            Current = Attendance.Count;
            Present = 0;
            OnTime = 0;
            Left = 0;
            if(Current != 0)
            {
                foreach (var item in Attendance)
                {
                    if(item.Status != Enums.AttendanceStatusEnum.AbsentExcused && item.Status != Enums.AttendanceStatusEnum.AbsentUnexcused)
                    {
                        Present++;
                    }
                    if(item.Status == Enums.AttendanceStatusEnum.OnTime || item.Status == Enums.AttendanceStatusEnum.OnTimeLeft)
                    {
                        OnTime++;
                    }
                    if(item.Status == Enums.AttendanceStatusEnum.LateLeft || item.Status == Enums.AttendanceStatusEnum.OnTimeLeft)
                    {
                        Left++;
                    }
                }
            }
        }
    }
}