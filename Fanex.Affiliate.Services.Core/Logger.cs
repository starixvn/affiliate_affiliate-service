namespace Fanex.Affiliate.Services.Core
{
    using System;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.Core.Util;
    using Fanex.Logging;

    /// <summary>
    /// FANEX Logging Wrapper.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// The error max.
        /// </summary>
        private const int ErrorMax = 10;

        /// <summary>
        /// The locker object.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The error count.
        /// </summary>
        private static int _errorCount;

        /// <summary>
        /// The current date.
        /// </summary>
        private static DateTime _currentDate = DateTime.MinValue.Date;

        /// <summary>
        /// Log info message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Info(string message)
        {
            if (AppSetting.LoggingMode == LoggingMode.All)
            {
                LogService.Info(message);
            }
        }

        /// <summary>
        /// Log error message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Error(string message)
        {
            if (AppSetting.LoggingMode != LoggingMode.Disable)
            {
                Error(message, null);
            }
        }

        /// <summary>
        /// Log exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public static void Error(Exception ex)
        {
            if (AppSetting.LoggingMode != LoggingMode.Disable)
            {
                string message = ExceptionFormatter.GetMessage(ex);
                Error(message, ex);
            }
        }

        /// <summary>
        /// Log error message with exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The exception.</param>
        public static void Error(string message, Exception ex)
        {
#if DEBUG
            ////throw ex;
#endif

            if (AppSetting.LoggingMode != LoggingMode.Disable)
            {
                lock (Locker)
                {
                    var today = DateTime.Today;
                    if (today != _currentDate)
                    {
                        _currentDate = today;
                        _errorCount = 0;
                    }
                }

                // limit logging 10 errors in a day
                if (_errorCount < ErrorMax)
                {
                    _errorCount++;
                    LogService.Error(message, ex);
                }
            }
        }
    }
}