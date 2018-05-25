using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreeAttendance.Models;

namespace TreeAttendance.Backend
{
    /// <summary>
    /// The interface for the SchoolDay DataSource.
    /// </summary>
    public interface SchoolDayInterface
    {
        SchoolDayModel Create(SchoolDayModel data);
        SchoolDayModel Read(string id);
        SchoolDayModel Update(SchoolDayModel data);
        bool Delete(string id);
        List<SchoolDayModel> Index();
        void Reset();
    }
}