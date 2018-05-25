using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TreeAttendance.Models
{
    /// <summary>
    /// This class represents a check-in record of a student on a schoolday, maintains check-in and check-out time pair. One AttendanceModel can have multiple AttendanceCheckInModels
    /// </summary>
    public class AttendanceCheckInModel
    {

        /// <summary>
        /// Clock-in time
        /// </summary>
        [Display(Name = "Check-in Time", Description = "Check-in time")]
        [Required(ErrorMessage = "Check-in time is required")]
        public TimeSpan CheckIn { get; set; }

        /// <summary>
        /// Clock-out time
        /// </summary>
        [Display(Name = "Check-out Time", Description = "Check-out time")]
        [Required(ErrorMessage = "Check-out time is required")]
        public TimeSpan CheckOut { get; set; }
        
        
        /// <summary>
        /// Constructor, call this when creating a check-in.
        /// </summary>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        public AttendanceCheckInModel(DateTime checkIn)
        {

            //Attendance.AttendanceCheckIns.Add(this);
            CheckIn = checkIn.TimeOfDay;
            CheckOut = SystemGlobals.Instance.DefaultEndTime;
        }
    }
}