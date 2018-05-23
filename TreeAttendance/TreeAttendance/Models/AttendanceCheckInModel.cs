using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TreeAttendance.Models
{
    /// <summary>
    /// This class represents a check-in record of a student on a schoolday, maintains clock-in and clock-out time pair.
    /// </summary>
    public class AttendanceCheckInModel
    {
        /// <summary>
        /// a unique id for this check-in record
        /// </summary>
        [Key]
        [Display(Name = "Id", Description = "Attendance Id")]
        [Required(ErrorMessage = "Id is required")]
        public String Id { get; set; }

        [Display(Name = "Check-in Time", Description = "Check-in time")]
        public string CheckIn { get; set; }

        [Display(Name = "Check-out Time", Description = "Check-out time")]
        public string CheckOut { get; set; }
        /// <summary>
        /// The Attendance model this check-in belongs to.
        /// </summary>
        public AttendanceModel Attendance { get; set; }
        /// <summary>
        /// Constuctor
        /// </summary>
        public AttendanceCheckInModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// Constructor, call this when creating a check-in.
        /// </summary>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        public AttendanceCheckInModel(AttendanceModel attendance, string checkIn, string checkOut)
        {
            Id = Guid.NewGuid().ToString();
            Attendance = attendance;
            //Attendance.AttendanceCheckIns.Add(this);
            CheckIn = checkIn;
            CheckOut = checkOut;
        }
    }
}