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
        /// a unique id for this check-in record
        /// </summary>
        [Key]
        [Display(Name = "Id", Description = "Attendance Id")]
        [Required(ErrorMessage = "Id is required")]
        public String Id { get; set; }

        /// <summary>
        /// Clock-in time
        /// </summary>
        [Display(Name = "Check-in Time", Description = "Check-in time")]
        [Required(ErrorMessage = "Check-in time is required")]
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Clock-out time
        /// </summary>
        [Display(Name = "Check-out Time", Description = "Check-out time")]
        [Required(ErrorMessage = "Check-out time is required")]
        public DateTime CheckOut { get; set; }
        
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
        public AttendanceCheckInModel(AttendanceModel attendance, DateTime checkIn, DateTime checkOut)
        {
            Id = Guid.NewGuid().ToString();
            Attendance = attendance;
            //Attendance.AttendanceCheckIns.Add(this);
            CheckIn = checkIn;
            CheckOut = checkOut;
        }
    }
}