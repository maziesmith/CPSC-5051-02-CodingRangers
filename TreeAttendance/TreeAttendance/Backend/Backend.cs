using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreeAttendance.Models;

namespace TreeAttendance.Backend
{   
    public static class Backend
    {
        public const double DefaultExpectedHours = 6;
        /// <summary>
        /// Maintains a StudentListModel
        /// </summary>
        public static StudentListModel StudentListBackend;
        /// <summary>
        /// Maintains a SchoolDayListModel
        /// </summary>
        public static SchoolDayListModel SchoolDayListBackend;

        /// <summary>
        /// Constructor, sets to initial state
        /// </summary>
        static Backend()
        {
            InitializeWithSeed();
        }
        /// <summary>
        /// Initializes, loads preset data (seed).
        /// </summary>
        public static void InitializeWithSeed()
        {
            StudentListBackend = new StudentListModel();
            SchoolDayListBackend = new SchoolDayListModel();
            //seed
            //create student profiles
            CreateStudent(new StudentModel("Allen", "boy1.png"));
            CreateStudent(new StudentModel("Mike", "boy2.png"));
            
            //create school days
            CreateSchoolDay(new SchoolDayModel("05/01/2018", DefaultExpectedHours));
            CreateSchoolDay(new SchoolDayModel("05/02/2018", DefaultExpectedHours));

            //create more student profiles
            CreateStudent(new StudentModel("Yanny", "girl1.png"));
            CreateStudent(new StudentModel("Laurel", "girl2.png"));

            //create more school days
            CreateSchoolDay(new SchoolDayModel("05/03/2018", DefaultExpectedHours));
            CreateSchoolDay(new SchoolDayModel("05/06/2018", DefaultExpectedHours));

            //some sample check-in records, used to generate check-in records ramdomly.
            //sample Check-in record, on time, stay
            void TypeA(AttendanceModel att)
            {
                CreateAttendanceCheckIn(new AttendanceCheckInModel(att, "08:45:00", "15:45:00"));
            };
            //sample Check-in record, late, stay
            void TypeB(AttendanceModel att)
            {
                CreateAttendanceCheckIn(new AttendanceCheckInModel(att, "09:15:00", "15:45:00"));
            };
            //sample Check-in record, late, left early
            void TypeC(AttendanceModel att)
            {
                CreateAttendanceCheckIn(new AttendanceCheckInModel(att, "09:15:00", "13:30:00"));
            };
            //sample Check-in record, absent unexcused
            void TypeD(AttendanceModel att)
            {
                CreateAttendanceCheckIn(new AttendanceCheckInModel(att, "", ""));
            };
            //sample Check-in record, absent excused
            void TypeE(AttendanceModel att)
            {
                CreateAttendanceCheckIn(new AttendanceCheckInModel(att, "", ""));
                att.ExcusedAbsence = true;
            };
            //sample Check-in record, multiple check-ins
            void TypeF(AttendanceModel att)
            {
                CreateAttendanceCheckIn(new AttendanceCheckInModel(att, "08:45:00", "10:30:00"));
                CreateAttendanceCheckIn(new AttendanceCheckInModel(att, "13:00:00", "15:45:00"));
            };

            Random rng = new Random();
            //generates check-in records randomly
            void Rng(AttendanceModel att)
            {
                int num = rng.Next(1, 7); //generates a random int between 1 and 6
                switch (num)
                {
                    case 1:
                        TypeA(att);
                        break;
                    case 2:
                        TypeB(att);
                        break;
                    case 3:
                        TypeC(att);
                        break;
                    case 4:
                        TypeD(att);
                        break;
                    case 5:
                        TypeE(att);
                        break;
                    default:
                        TypeF(att);
                        break;
                }
            }


            //Create one Check-in for each Attendance
            foreach (var schd in SchoolDayListBackend.SchoolDayList)
            {
                foreach (var att in schd.AttendanceList)
                {
                    //generates check-in records randomly
                    Rng(att);
                }
            }
        }
        /// <summary>
        /// gets the StudemtModel with the given id
        /// </summary>
        /// <param name="id">id of the student</param>
        /// <returns>StudentModel</returns>
        internal static StudentModel GetStudentModel(string id)
        {
            foreach (var stu in StudentListBackend.StudentList)
            {
                if (stu.Id.Equals(id))
                {
                    return stu;
                }
            }
            return null;
        }
        /// <summary>
        /// creates a StudentModel with given student
        /// </summary>
        /// <param name="student"></param>
        internal static void CreateStudent(StudentModel student)
        {
            StudentListBackend.StudentList.Add(student);
        }
        /// <summary>
        /// edits a StudentModel with given student
        /// </summary>
        /// <param name="student">student to edit</param>
        internal static void EditStudent(StudentModel student)
        {
            string id = student.Id;
            StudentModel stu = GetStudentModel(id);
            stu.Name = student.Name;
            stu.ProfilePictureUri = student.ProfilePictureUri;
        }
        /// <summary>
        /// deletes the given student
        /// </summary>
        /// <param name="student">student to delete</param>
        internal static void DeleteStudent(StudentModel student)
        {
            StudentModel stu = GetStudentModel(student.Id);
            StudentListBackend.StudentList.Remove(stu);
            //deletes attendance records of this student from schooldays' attendancelist
            foreach(var att in stu.AttendanceList)
            {
                SchoolDayModel schd = GetSchoolDayModel(att.SchoolDay.Id);
                schd.AttendanceList.Remove(att);
            }
        }
        /// <summary>
        /// gets the SchoolDayModel
        /// </summary>
        /// <param name="id">the id of the school day</param>
        /// <returns>SchoolDayModel</returns>
        internal static SchoolDayModel GetSchoolDayModel(string id)
        {
            foreach (var schD in SchoolDayListBackend.SchoolDayList)
            {
                if (schD.Id.Equals(id))
                {
                    return schD;
                }
            }

            return null;
        }
        /// <summary>
        /// Creates a new school day using the given SchoolDayModel
        /// </summary>
        /// <param name="date">SchoolDayModel to create from</param>
        internal static void CreateSchoolDay(SchoolDayModel schoolDay)
        {
            SchoolDayListBackend.SchoolDayList.Add(schoolDay);
            //create attendance records based on current student list
            foreach (var student in StudentListBackend.StudentList)
            {
                AttendanceModel att = new AttendanceModel(student, SchoolDayListBackend.SchoolDayList.Last());
                //add the attendance to the AttendanceList of the school day
                SchoolDayListBackend.SchoolDayList.Last().AttendanceList.Add(att);
                //add the attendance to the AttendanceList of the student
                student.AttendanceList.Add(att);
            }
        }
        /// <summary>
        /// edits a SchoolDayModel with given schoolDay
        /// </summary>
        /// <param name="student">student to edit</param>
        internal static void EditSchoolDay(SchoolDayModel schoolDay)
        {
            string id = schoolDay.Id;
            SchoolDayModel schd = GetSchoolDayModel(id);
            schd.Date = schoolDay.Date;
            schd.ExpectedHours = schoolDay.ExpectedHours;
        }
        /// <summary>
        /// Deletes the given SchoolDayModel
        /// </summary>
        /// <param name="schoolDay">The model to delete</param>
        internal static void DeleteSchoolDay(SchoolDayModel schoolDay)
        {
            SchoolDayModel schd = GetSchoolDayModel(schoolDay.Id);
            SchoolDayListBackend.SchoolDayList.Remove(schd);
            //deletes attendance records of this school day from students' attendancelist
            foreach (var att in schd.AttendanceList)
            {
                StudentModel stu = GetStudentModel(att.Student.Id);
                stu.AttendanceList.Remove(att);
            }
        }
        /// <summary>
        /// gets the AtendanceModel with the given id
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <returns>AttendanceModel</returns>
        internal static AttendanceModel GetAttendanceModel(string id)
        {
            foreach (var schD in SchoolDayListBackend.SchoolDayList)
            {
                foreach (var att in schD.AttendanceList)
                {
                    if (att.Id.Equals(id))
                    {
                        return att;
                    }
                }
            }

            return null;
        }
        /// <summary>
        /// creates an AttendanceModel using the given attendance
        /// </summary>
        /// <param name="attendance">AttendanceModel to create from</param>
        internal static void CreateAttendance(AttendanceModel attendance)
        {
            string schdId = attendance.SchoolDay.Id;
            string stuId = attendance.Student.Id;
            SchoolDayModel schd = GetSchoolDayModel(schdId);
            StudentModel stu = GetStudentModel(stuId);
            //add the attendance to the AttendanceList of the school day
            schd.AttendanceList.Add(attendance);
            //add the attendance to the AttendanceList of the student
            stu.AttendanceList.Add(attendance);
        }
        /// <summary>
        /// edits the given attendance
        /// </summary>
        /// <param name="attendance">the AttendanceModel to edit</param>
        internal static void EditAttendance(AttendanceModel attendance)
        {
            string id = attendance.Id;
            AttendanceModel att = GetAttendanceModel(id);
            att.ExcusedAbsence = attendance.ExcusedAbsence;
        }
        /// <summary>
        /// Deletes the given attendance
        /// </summary>
        /// <param name="attendance">AttendanceModel to delete</param>
        internal static void DeleteAttendance(AttendanceModel attendance)
        {
            AttendanceModel att = GetAttendanceModel(attendance.Id);
            SchoolDayModel schd = GetSchoolDayModel(att.SchoolDay.Id);
            StudentModel stu = GetStudentModel(att.Student.Id);
            schd.AttendanceList.Remove(att);
            stu.AttendanceList.Remove(att);
        }
        /// <summary>
        /// Gets the AttendanceCheckInModel
        /// </summary>
        /// <param name="id">id of the AttendanceCheckInModel</param>
        /// <returns></returns>
        internal static AttendanceCheckInModel GetAttendanceCheckIn(string id)
        {
            foreach (var stu in StudentListBackend.StudentList)
            {
                foreach (var att in stu.AttendanceList)
                {
                    foreach (var checkIn in att.AttendanceCheckIns)
                    {
                        if (checkIn.Id.Equals(id))
                        {
                            return checkIn;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Creates an AttendanceCheckInModel
        /// </summary>
        /// <param name="checkIn">AttendanceCheckInModel to create from</param>
        /// <returns></returns>
        internal static void CreateAttendanceCheckIn(AttendanceCheckInModel checkIn)
        {
            AttendanceModel attendance = GetAttendanceModel(checkIn.Attendance.Id);
            attendance.AttendanceCheckIns.Add(checkIn);           
        }
        /// <summary>
        /// Edits an AttendanceCheckInModel
        /// </summary>
        /// <param name="checkIn">AttendanceCheckInModel to edit</param>
        /// <returns></returns>
        internal static void EditAttendanceCheckIn(AttendanceCheckInModel checkIn)
        {
            AttendanceCheckInModel CheckIn = GetAttendanceCheckIn(checkIn.Id);
            CheckIn.CheckIn = checkIn.CheckIn;
            CheckIn.CheckOut = checkIn.CheckOut;
        }
        /// <summary>
        /// Deletes an AttendanceCheckInModel
        /// </summary>
        /// <param name="checkIn">AttendanceCheckInModel to delete</param>
        /// <returns></returns>
        internal static void DeleteAttendanceCheckIn(AttendanceCheckInModel checkIn)
        {
            AttendanceCheckInModel CheckIn = GetAttendanceCheckIn(checkIn.Id);
            AttendanceModel attendance = GetAttendanceModel(checkIn.Attendance.Id);
            attendance.AttendanceCheckIns.Remove(CheckIn);            
        }

    }
}