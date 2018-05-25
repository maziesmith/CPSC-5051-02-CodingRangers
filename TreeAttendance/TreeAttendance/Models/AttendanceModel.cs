using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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

        ///// <summary>
        ///// True if this is and excused absence.
        ///// </summary>
        //public enum ExcusedAbsence { }
        
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
            //Student.AttendanceList.Add(this);
            SchoolDayId = schoolDay;
            //SchoolDay.AttendanceList.Add(this);
        }
    }
}