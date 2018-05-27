using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TreeAttendance.Models.Enums;

namespace TreeAttendance.Models
{
    //this class models an attendance record of a particular student on a particular day
    public class AttendanceModel
    {
        /// <summary>
        /// a unique id for this attendance record
        /// </summary>
        [Key]
        [Display(Name = "Id", Description = "Attendance Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        /// <summary>
        /// The school day of this attendance record
        /// </summary>
        public string SchoolDayId { get; set; }

        /// <summary>
        /// The student of the attendance record
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// The status of the attendance, for example currently logged in, out
        /// </summary>
        [Display(Name = "Current Status", Description = "Status of the attendance")]
        [Required(ErrorMessage = "Status is required")]
        public AttendanceStatusEnum Status { get; set; }

        /// <summary>
        /// Maintains a list of AttendanceCheckInModel. If there are 3 check-ins on this day by this student, then the list size will be 3.
        /// </summary>
        public List<AttendanceCheckInModel> AttendanceCheckIns { get; set; }

        /// <summary>
        /// Id will be random, AttendanceChekcIns will be of size 1.
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            AttendanceCheckIns = new List<AttendanceCheckInModel>();
            //absent unexcused by default
            Status = AttendanceStatusEnum.AbsentUnexcused;
        }

        /// <summary>
        /// Constuctor, Student and SchoolDay not set.
        /// </summary>
        public AttendanceModel()
        {
            Initialize();
        }

        /// <summary>
        /// Constructor, call this when creating an attendance record
        /// </summary>
        /// <param name="student"></param>
        /// <param name="schoolDay"></param>
        public AttendanceModel(string student, string schoolDay)
        {
            Initialize();
            StudentId = student;
            SchoolDayId = schoolDay;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        public void CheckIn(DateTime time)
        {
            AttendanceCheckIns.Add(new AttendanceCheckInModel(time.TimeOfDay));
            ComputeStatus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        public void CheckOut(DateTime time)
        {
            AttendanceCheckIns.Last().CheckOut = time.TimeOfDay;
            ComputeStatus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <param name="index"></param>
        public void Edit(TimeSpan checkIn, TimeSpan checkOut, int index)
        {
            AttendanceCheckIns[index].CheckIn = checkIn;
            AttendanceCheckIns[index].CheckOut = checkOut;
            ComputeStatus();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ComputeStatus()
        {
            //if checked in on time
            if (TimeSpan.Compare(AttendanceCheckIns.First().CheckIn, SystemGlobals.Instance.DefaultStartTime) == 1)
            {
                if (TimeSpan.Compare(AttendanceCheckIns.First().CheckOut, SystemGlobals.Instance.DefaultEndTime) >= 0)
                {
                    Status = AttendanceStatusEnum.Late;
                }
                else
                {
                    Status = AttendanceStatusEnum.LateLeft;
                }
            }
            else
            {
                if (TimeSpan.Compare(AttendanceCheckIns.First().CheckOut, SystemGlobals.Instance.DefaultEndTime) >= 0)
                {
                    Status = AttendanceStatusEnum.OnTime;
                }
                else
                {
                    Status = AttendanceStatusEnum.OnTimeLeft;
                }
            }
        }

        public void SetExcused()
        {
            Status = AttendanceStatusEnum.AbsentExcused;
        }

        public void SetUnExcused()
        {
            Status = AttendanceStatusEnum.AbsentUnexcused;
        }
    }
}