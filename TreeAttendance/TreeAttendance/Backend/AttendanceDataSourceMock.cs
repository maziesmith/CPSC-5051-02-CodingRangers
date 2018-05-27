using System;
using System.Collections.Generic;
using System.Linq;
using TreeAttendance.Models;
using TreeAttendance.Models.ViewModels;

namespace TreeAttendance.Backend
{
    /// <summary>
    /// Holds the Attendance Data as a Mock Data set, used for Unit Testing, System Testing, Offline Development etc.
    /// </summary>
    public class AttendanceDataSourceMock : AttendanceInterface
    {
        /// <summary>
        /// Make into a singleton
        /// </summary>
        private static volatile AttendanceDataSourceMock instance;
        private static object syncRoot = new Object();

        private AttendanceDataSourceMock() { }

        public static AttendanceDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new AttendanceDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The Data for the Attendances
        /// </summary>
        private List<AttendanceModel> AttendanceList = new List<AttendanceModel>();

        /// <summary>
        /// Makes a new Attendance
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Attendance Passed In</returns>
        public AttendanceModel Create(AttendanceModel data)
        {       
            AttendanceList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public AttendanceModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = AttendanceList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public AttendanceModel CreateCheckIn(AttendanceCheckInViewModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = AttendanceList.Find(n => n.Id == data.AttendanceId);
            myReturn.AttendanceCheckIns.Add(new AttendanceCheckInModel(SystemGlobals.Instance.DefaultStartTime));
            myReturn.Edit(data.CheckIn, data.CheckOut, data.Index);

            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public AttendanceModel UpdateCheckIn(AttendanceCheckInViewModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = AttendanceList.Find(n => n.Id == data.AttendanceId);

            myReturn.Edit(data.CheckIn, data.CheckOut, data.Index);

            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public bool DeleteCheckIn(string id, int index)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            var myData = AttendanceList.Find(n => n.Id == id);

            return myData.Delete(index);
        }

        /// <summary>
        /// Remove the Data item if it is in the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for success, else false</returns>
        public bool Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myData = AttendanceList.Find(n => n.Id == Id);
            var myReturn = AttendanceList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Attendances</returns>
        public List<AttendanceModel> Index()
        {
            return AttendanceList;
        }

        /// <summary>
        /// Return a subset of the dataset by the given student
        /// </summary>
        /// <returns>List of Attendances</returns>
        public List<AttendanceModel> IndexByStudent(string id)
        {
            List<AttendanceModel> list = new List<AttendanceModel>();
            foreach (var item in AttendanceList)
            {
                if (item.StudentId == id)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Return a subset of the dataset by the given school day
        /// </summary>
        /// <returns>List of Attendances</returns>
        public List<AttendanceModel> IndexBySchoolDay(string id)
        {
            List<AttendanceModel> list = new List<AttendanceModel>();
            foreach (var item in AttendanceList)
            {
                if(item.SchoolDayId == id)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public bool CheckIn(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            var myData = AttendanceList.Find(n => n.Id == id);
            myData.CheckIn(DateTime.Now);
            return true;
        }

        public bool CheckOut(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            var myData = AttendanceList.Find(n => n.Id == id);
            myData.CheckOut(DateTime.Now);
            return true;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            AttendanceList.Clear();
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            foreach (var item in SchoolDayBackend.Instance.Index())
            {
                foreach (var item2 in StudentBackend.Instance.Index())
                {
                    //create attendance and use rng to create check-in records
                    Rng(Create(new AttendanceModel(item2.Id, item.Id)));
                }
            }
        }
        //some sample check-in records, used to generate check-in records ramdomly.
        //sample Check-in record, on time, stay
        void TypeA(AttendanceModel att)
        {
            att.CheckIn(DateTime.Parse("08:37:00"));
        }
        //sample Check-in record, late, stay
        void TypeB(AttendanceModel att)
        {
            att.CheckIn(DateTime.Parse("09:15:00"));
        }
        //sample Check-in record, late, left early
        void TypeC(AttendanceModel att)
        {
            att.CheckIn(DateTime.Parse("09:15:00"));
            att.CheckOut(DateTime.Parse("12:00:00"));
        }
        //sample Check-in record, absent unexcused
        void TypeD(AttendanceModel att)
        {
        }
        //sample Check-in record, absent excused
        void TypeE(AttendanceModel att)
        {
            att.SetExcused();
        }
        //sample Check-in record, multiple check-ins
        void TypeF(AttendanceModel att)
        {
            att.CheckIn(DateTime.Parse("08:37:00"));
            att.CheckOut(DateTime.Parse("09:45:00"));
            att.CheckIn(DateTime.Parse("13:00:00"));
        }

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
    }
}