namespace TreeAttendance.Models.Enums
{
    /// <summary>
    /// Check-in Status Options
    /// </summary>
    public enum AttendanceStatusEnum
    {
        //Absent unexcused
        AbsentUnexcused = 1,

        //arrived on time
        OnTime = 2,

        //arrived late
        Late = 3,

        //left
        OnTimeLeft = 4,

        //left
        LateLeft = 5,

        //Absent excused
        AbsentExcused = 6,
    }
}