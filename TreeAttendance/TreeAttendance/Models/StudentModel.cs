using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TreeAttendance.Models
{
    public class StudentModel
    {
        /// <summary>
        /// The ID for the student
        /// </summary>
        [Key]
        [Display(Name = "Id", Description = "Student Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        /// <summary>
        /// The Friendly name for the student
        /// </summary>
        [Display(Name = "Name", Description = "Nick Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// Url of the profile picture, if url = "profile.png", the file can be found under "Content/Img" folder
        /// </summary>
        [Display(Name = "Profile", Description = "Profile Picture")]
        [Required(ErrorMessage = "Profile picture is required")]
        public string ProfilePictureUri { get; set; }

        /// <summary>
        /// The Tree stage, a number from 1 to 5
        /// </summary>
        [Display(Name = "TreeStage", Description = "Tree Stage")]
        public int TreeStage { get; set; }

        /// <summary>
        /// The leaf count, a number from 1 to 20
        /// </summary>
        [Display(Name = "LeafCount", Description = "Leaf Count")]
        public int LeafCount { get; set; }

        /// <summary>
        /// A list of this student's attendance records
        /// </summary>
        public List<AttendanceModel> AttendanceList { get; set; }

        /// <summary>
        /// The default for new student
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            TreeStage = 1;
            LeafCount = 0;
            ProfilePictureUri = "boy1.png";
            AttendanceList = new List<AttendanceModel>();
        }

        /// <summary>
        /// Constructor for a student
        /// </summary>
        public StudentModel()
        {
            Initialize();
        }

        /// <summary>
        /// Constructor for Student.  Call this when making a new student
        /// </summary>
        /// <param name="name">The Name to call the student</param>
        /// <param name="id">The ID, must be unique</param>
        /// <param name="profileUrl">The profile picture url</param>

        public StudentModel(string name, string profileUrl)
        {
            Initialize();
            Name = name;
            ProfilePictureUri = profileUrl;
        }
    }
}