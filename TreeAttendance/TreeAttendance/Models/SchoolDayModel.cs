using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TreeAttendance.Models
{
    public class SchoolDayModel
    {
        /// <summary>
        /// The ID for the school day
        /// </summary>
        [Key]
        [Display(Name = "Id", Description = "School day Id")]
        [Required(ErrorMessage = "Id is required")]
        public String Id { get; set; }
        /// <summary>
        /// The date of this school day
        /// </summary>
        [Display(Name = "Date", Description = "Date Id")]
        [Required(ErrorMessage = "Date required")]
        public string Date { get; set; }
        /// <summary>
        /// Maintains a list of attendance of this school day.
        /// </summary>
        public List<AttendanceModel> AttendanceList { get; set; }

        /// <summary>
        /// Constructor, Id is randomly generated
        /// </summary>
        public SchoolDayModel()
        {
            AttendanceList = new List<AttendanceModel>();
            Id = Guid.NewGuid().ToString();
        }
    }
}