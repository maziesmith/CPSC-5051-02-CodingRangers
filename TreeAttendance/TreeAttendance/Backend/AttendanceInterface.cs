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
        bool DeleteCheckIn(string id, int index);
        bool Delete(string id);
        List<AttendanceModel> Index();
        List<AttendanceModel> IndexByStudent(string id);
        List<AttendanceModel> IndexBySchoolDay(string id);
        bool CheckIn(string id);
        bool CheckOut(string id);
        void Reset();
    }
}