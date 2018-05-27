using System.Collections.Generic;
using TreeAttendance.Models;
using TreeAttendance.Models.ViewModels;

namespace TreeAttendance.Backend
{
    /// <summary>
    /// The interface for the Attendance DataSource.
    /// </summary>
    public interface AttendanceInterface
    {
        AttendanceModel Create(AttendanceModel data);
        AttendanceModel CreateCheckIn(AttendanceCheckInViewModel data);
        AttendanceModel Read(string id);
        AttendanceModel UpdateCheckIn(AttendanceCheckInViewModel data);
        bool Delete(string id);
        List<AttendanceModel> Index();
        List<AttendanceModel> IndexByStudent(string id);
        List<AttendanceModel> IndexBySchoolDay(string id);
        void Reset();
    }
}