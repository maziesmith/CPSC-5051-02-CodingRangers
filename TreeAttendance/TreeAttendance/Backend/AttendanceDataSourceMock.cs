using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreeAttendance.Models;

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
        public AttendanceModel Update(AttendanceModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = AttendanceList.Find(n => n.Id == data.Id);

            myReturn.SchoolDayId = data.SchoolDayId;
            myReturn.StudentId = data.StudentId;

            return myReturn;
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

        }
    }
}