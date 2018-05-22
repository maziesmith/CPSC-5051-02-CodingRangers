using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeAttendance.Models
{
    public class SchoolDayListModel
    {
        /// <summary>
        /// Maintains a list of all school days so far
        /// </summary>
        public List<SchoolDayModel> SchoolDayList { get; set; }
        /// <summary>
        /// Constructor, The list is empty initially
        /// </summary>
        public SchoolDayListModel()
        {
            SchoolDayList = new List<SchoolDayModel>();
        }
    }
}