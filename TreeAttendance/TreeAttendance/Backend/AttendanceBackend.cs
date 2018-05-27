using System;
using System.Collections.Generic;
using System.Linq;
using TreeAttendance.Models;
using TreeAttendance.Models.Enums;
using TreeAttendance.Models.ViewModels;

namespace TreeAttendance.Backend
{
    /// <summary>
    /// Attendance Backend handles the business logic and data for Attendances
    /// </summary>
    public class AttendanceBackend
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile AttendanceBackend instance;
        private static object syncRoot = new Object();

        private AttendanceBackend() { }

        public static AttendanceBackend Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new AttendanceBackend();
                            SetDataSource(SystemGlobals.Instance.DataSourceValue);
                        }
                    }
                }

                return instance;
            }
        }

        // Get the Datasource to use
        private static AttendanceInterface DataSource;

        /// <summary>
        /// Switches between Live, and Mock Datasets
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        public static void SetDataSource(DataSourceEnum dataSourceEnum)
        {
            if (dataSourceEnum == DataSourceEnum.SQL)
            {
                // SQL not hooked up yet...
                throw new NotImplementedException();
            }

            // Default is to use the Mock
            DataSource = AttendanceDataSourceMock.Instance;
        }

        /// <summary>
        /// Makes a new Attendance
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Attendance Passed In</returns>
        public AttendanceModel Create(AttendanceModel data)
        {
            DataSource.Create(data);
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

            var myReturn = DataSource.Read(id);
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

            var myData = DataSource.Read(data.AttendanceId);
            if (myData == null)
            {
                // Not found
                return null;
            }

            // Update the record
            var myReturn = DataSource.CreateCheckIn(data);

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

            var myData = DataSource.Read(data.AttendanceId);
            if (myData == null)
            {
                // Not found
                return null;
            }

            // Update the record
            var myReturn = DataSource.UpdateCheckIn(data);

            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public bool DeleteCheckIn(string Id, int Index)
        {

            var myData = DataSource.Read(Id);
            if (myData == null)
            {
                // Not found
                return false;
            }

            // Update the record
            var myReturn = DataSource.DeleteCheckIn(Id, Index);
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

            var myReturn = DataSource.Delete(Id);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Attendances</returns>
        public List<AttendanceModel> Index()
        {
            var myData = DataSource.Index();
            return myData;
        }

        /// <summary>
        /// Return a subset of the dataset of the school day
        /// </summary>
        /// <returns>List of Attendances</returns>
        public List<AttendanceModel> IndexBySchoolDay(string id)
        {
            var myData = DataSource.IndexBySchoolDay(id);
            return myData;
        }

        /// <summary>
        /// Return a subset of the dataset of the student
        /// </summary>
        /// <returns>List of Attendances</returns>
        public List<AttendanceModel> IndexByStudent(string id)
        {
            var myData = DataSource.IndexByStudent(id);
            return myData;
        }

        /// <summary>
        /// Helper function that resets the DataSource, and rereads it.
        /// </summary>
        public void Reset()
        {
            DataSource.Reset();
        }

        /// <summary>
        /// Returns the First Attendance in the system
        /// </summary>
        /// <returns>Null or valid data</returns>
        public AttendanceModel GetDefault()
        {
            var myReturn = DataSource.Index().First();
            return myReturn;
        }
    }
}