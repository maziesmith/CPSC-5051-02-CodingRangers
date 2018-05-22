using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TreeAttendance.Models
{
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
        public SchoolDayModel SchoolDay { get; set; }
        /// <summary>
        /// The student of the attendance record
        /// </summary>
        public StudentModel Student { get; set; }
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

            AttendanceCheckIns.Add(new AttendanceCheckInModel());
            AttendanceCheckIns.Last().AttendanceModel = this;
        }
        /// <summary>
        /// Constuctor, Student and SchoolDay not set.
        /// </summary>
        public AttendanceModel()
        {
            Initialize();
        }
        /// <summary>
        /// Set clock-in time.
        /// </summary>
        /// <param name="time"></param>
        public void SetClockInTime(DateTime time)
        {
            AttendanceCheckIns.Last().ClockIn = time;
        }
        /// <summary>
        /// Set clock-out time. After the time is set, a new AttendanceCheckInModel is created a the end of the AttendanceCheckIns list.
        /// </summary>
        /// <param name="time"></param>
        public void SetClockOutTime(DateTime time)
        {
            AttendanceCheckIns.Last().ClockOut = time;
            AttendanceCheckIns.Add(new AttendanceCheckInModel());
            AttendanceCheckIns.Last().AttendanceModel = this;
        }
    }
}