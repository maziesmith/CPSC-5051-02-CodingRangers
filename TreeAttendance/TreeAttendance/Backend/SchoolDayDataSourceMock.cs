using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreeAttendance.Models;

namespace TreeAttendance.Backend
{
    /// <summary>
    /// Holds the SchoolDay Data as a Mock Data set, used for Unit Testing, System Testing, Offline Development etc.
    /// </summary>
    public class SchoolDayDataSourceMock : SchoolDayInterface
    {
        /// <summary>
        /// Make into a singleton
        /// </summary>
        private static volatile SchoolDayDataSourceMock instance;
        private static object syncRoot = new Object();

        private SchoolDayDataSourceMock() { }

        public static SchoolDayDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SchoolDayDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The Data for the SchoolDays
        /// </summary>
        private List<SchoolDayModel> SchoolDayList = new List<SchoolDayModel>();

        /// <summary>
        /// Makes a new SchoolDay
        /// </summary>
        /// <param name="data"></param>
        /// <returns>SchoolDay Passed In</returns>
        public SchoolDayModel Create(SchoolDayModel data)
        {
            SchoolDayList.Add(data);

            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public SchoolDayModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = SchoolDayList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public SchoolDayModel Update(SchoolDayModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = SchoolDayList.Find(n => n.Id == data.Id);

            myReturn.ExpectedHours = data.ExpectedHours;

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

            var myData = SchoolDayList.Find(n => n.Id == Id);
            var myReturn = SchoolDayList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of SchoolDays</returns>
        public List<SchoolDayModel> Index()
        {
            return SchoolDayList;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            SchoolDayList.Clear();
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            //create school days
            for (int i = 50; i >= 0; i--)
            {
                DateTime date = SystemGlobals.Instance.Today.AddDays(-i);
                if((date.DayOfWeek != DayOfWeek.Saturday) && (date.DayOfWeek != DayOfWeek.Sunday))
                {
                    Create(new SchoolDayModel(SystemGlobals.Instance.Today.AddDays(-i), new TimeSpan(7, 0, 0)));
                }               
            }
        }
    }
}