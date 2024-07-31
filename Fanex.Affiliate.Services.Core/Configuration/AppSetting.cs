namespace Fanex.Affiliate.Services.Core.Configuration
{
    using System;
    using System.Configuration;
    using System.Reflection;
    using Fanex.Affiliate.Services.Core.Util;
    using NodaTime;

    /// <summary>
    /// Application Setting.
    /// </summary>
    public static class AppSetting
    {
        /// <summary>
        /// Gets AdminID passed as parameter when calling stored procedure for importing.
        /// </summary>
        /// <value>
        /// The admin ID.
        /// </value>
        public static int AdminId { get; private set; }

        /// <summary>
        /// Gets cache expire time.
        /// </summary>
        /// <value>
        /// The expired cache time.
        /// </value>
        public static int ExpiredCacheTime { get; private set; }

        /// <summary>
        /// Gets a value indicating whether application environment is local or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the environment is local test; otherwise, <c>false</c>.
        /// </value>
        public static bool LocalTest { get; private set; }

        /// <summary>
        /// Gets Logging Mode.
        /// </summary>
        /// <value>
        /// The logging mode.
        /// </value>
        public static LoggingMode LoggingMode { get; private set; }

        /// <summary>
        /// Gets the time to import data for yesterday.
        /// </summary>
        /// <value>
        /// The import exchange rate standard time.
        /// </value>
        public static LocalTime ImportExchangeRateStdTime { get; private set; }

        /// <summary>
        /// Gets the time to import data for yesterday.
        /// </summary>
        /// <value>
        /// The import member information standard time.
        /// </value>
        public static LocalTime ImportMemberInfoStdTime { get; private set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            AdminId = 9999;
            ExpiredCacheTime = 10000; // seconds
            LocalTest = Convert.ToInt32(ConfigurationManager.AppSettings["LocalTest"]) == 1;
            LoggingMode = (LoggingMode)Convert.ToInt32(ConfigurationManager.AppSettings["LoggingMode"]);

            ImportExchangeRateStdTime = DateTimeUtil.GetLocalTime(ConfigurationManager.AppSettings["ImportExchangeRateStdTime"]);
            ImportMemberInfoStdTime = DateTimeUtil.GetLocalTime(ConfigurationManager.AppSettings["ImportMemberInfoStdTime"]);

            // logging
            string message = "Application Settings initialized:";
            Type type = MethodBase.GetCurrentMethod().DeclaringType;
            foreach (var p in type.GetProperties())
            {
                var name = p.Name;
                var value = p.GetValue(null, null);
                message += string.Format("\n{0}: {1}", name, value);
            }
        }
    }
}