using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreeAttendance.Models;

namespace TreeAttendance.Backend
{
    /// <summary>
    /// The interface for the Attendance DataSource.
    /// </summary>
    public interface AttendanceInterface
    {
        AttendanceModel Create(AttendanceModel data);
        AttendanceModel Read(string id);
        AttendanceModel Update(AttendanceModel data);
        bool Delete(string id);
        List<AttendanceModel> Index();
        List<AttendanceModel> IndexByStudent(string id);
        List<AttendanceModel> IndexBySchoolDay(string id);
        void Reset();
    }
}