using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TreeAttendance.Models.Enums;

namespace TreeAttendance.Models
{
    /// <summary>
    /// System wide Global variables
    /// </summary>
    public class SystemGlobals
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile SystemGlobals instance;
        private static object syncRoot = new Object();

        private SystemGlobals() { }

        public static SystemGlobals Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SystemGlobals();
                        }
                    }
                }

                return instance;
            }
        }

        // The Enum to use for the current data source
        // Default to Mock
        public DataSourceEnum DataSourceValue = DataSourceEnum.Mock;

        //The default expected hours of a school day
        public TimeSpan DefaultExpectedHours = new TimeSpan(7, 0, 0);

        //The default start time of a school day
        public TimeSpan DefaultStartTime = new TimeSpan(8, 45, 0);

        //The default end time of a school day
        public TimeSpan DefaultEndTime = new TimeSpan(15, 45, 0);

        //Today's date, for demo purposes
        public DateTime Today = DateTime.Now.Date;
    }
}