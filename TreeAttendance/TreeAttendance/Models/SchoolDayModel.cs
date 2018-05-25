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
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
        
        /// <summary>
        /// The expected hours of this school day.
        /// </summary>
        [Display(Name = "Expected Hours", Description = "Expected hours")]
        [Required(ErrorMessage = "Expected Hours is required")]
        public TimeSpan ExpectedHours { get; set; }
        
        /// <summary>
        /// Constructor, Id is randomly generated
        /// </summary>
        public SchoolDayModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        /// <summary>
        /// Constructor, call this when creating a school day.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="expectedHours"></param>
        public SchoolDayModel(DateTime date, TimeSpan expectedHours)
        {
            Id = Guid.NewGuid().ToString();
            Date = date.Date;
            ExpectedHours = expectedHours;
        }
    }
}