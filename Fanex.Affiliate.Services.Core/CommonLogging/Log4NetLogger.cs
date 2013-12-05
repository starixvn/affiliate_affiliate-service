// --------------------------------------------------------------------------
// <copyright file="Log4NetLogger.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Core.CommonLogging
{
    using System;
    using System.Diagnostics;
    using Common.Logging;
    using Common.Logging.Factory;
    using log4net.Core;

    /// <summary>
    /// Log4Net Logger.
    /// </summary>
    [Serializable]
    public class Log4NetLogger : AbstractLogger
    {
        #region Fields

        /// <summary>
        /// The caller stack boundary type.
        /// </summary>
        private static Type _callerStackBoundaryType;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The log object.
        /// </summary>
        private readonly object _lockObject;

        #endregion Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetLogger"/> class.
        /// </summary>
        /// <param name="log">The logger.</param>
        protected internal Log4NetLogger(ILoggerWrapper log)
        {
            _logger = log.Logger;
            _lockObject = new object();
        }

        #region ILog Members

        /// <summary>
        /// Gets a value indicating whether [is trace enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is trace enabled]; otherwise, <c>false</c>.
        /// </value>
        public override bool IsTraceEnabled
        {
            get { return _logger.IsEnabledFor(Level.Trace); }
        }

        /// <summary>
        /// Gets a value indicating whether [is debug enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is debug enabled]; otherwise, <c>false</c>.
        /// </value>
        public override bool IsDebugEnabled
        {
            get { return _logger.IsEnabledFor(Level.Debug); }
        }

        /// <summary>
        /// Gets a value indicating whether [is information enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [is information enabled]; otherwise, <c>false</c>.
        /// </value>
        public override bool IsInfoEnabled
        {
            get { return _logger.IsEnabledFor(Level.Info); }
        }

        /// <summary>
        /// Gets a value indicating whether [is warn enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is warn enabled]; otherwise, <c>false</c>.
        /// </value>
        public override bool IsWarnEnabled
        {
            get { return _logger.IsEnabledFor(Level.Warn); }
        }

        /// <summary>
        /// Gets a value indicating whether [is error enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is error enabled]; otherwise, <c>false</c>.
        /// </value>
        public override bool IsErrorEnabled
        {
            get { return _logger.IsEnabledFor(Level.Error); }
        }

        /// <summary>
        /// Gets a value indicating whether [is fatal enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is fatal enabled]; otherwise, <c>false</c>.
        /// </value>
        public override bool IsFatalEnabled
        {
            get { return _logger.IsEnabledFor(Level.Fatal); }
        }

        /// <summary>
        /// Maps <see cref="LogLevel" /> to log4net's <see cref="Level" />
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <returns>Returns log level.</returns>
        public static Level GetLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.All:
                    return Level.All;
                case LogLevel.Trace:
                    return Level.Trace;
                case LogLevel.Debug:
                    return Level.Debug;
                case LogLevel.Info:
                    return Level.Info;
                case LogLevel.Warn:
                    return Level.Warn;
                case LogLevel.Error:
                    return Level.Error;
                case LogLevel.Fatal:
                    return Level.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("logLevel", logLevel, "unknown log level");
            }
        }

        /// <summary>
        /// Actually sends the message to the underlying log system.
        /// </summary>
        /// <param name="logLevel">The level of this log event.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log (may be null).</param>
        protected override void WriteInternal(LogLevel logLevel, object message, Exception exception)
        {
            // Determine correct caller - this might change due to jit optimizations with method inlining
            if (_callerStackBoundaryType == null)
            {
                lock (_lockObject)
                {
                    var stack = new StackTrace();
                    var thisType = GetType();
                    _callerStackBoundaryType = typeof(AbstractLogger);
                    for (int i = 1; i < stack.FrameCount; i++)
                    {
                        if (!IsInTypeHierarchy(thisType, stack.GetFrame(i).GetMethod().DeclaringType))
                        {
                            _callerStackBoundaryType = stack.GetFrame(i - 1).GetMethod().DeclaringType;
                            break;
                        }
                    }
                }
            }

            Level level = GetLevel(logLevel);
            _logger.Log(_callerStackBoundaryType, level, message, exception);

            if (logLevel == LogLevel.Error)
            {
                Logger.Error((string)message, exception);
            }
        }

        /// <summary>
        /// Determines whether [is in type hierarchy] [the specified current type].
        /// </summary>
        /// <param name="currentType">Type of the current.</param>
        /// <param name="checkType">Type of the check.</param>
        /// <returns>
        ///   <c>true</c> if [is in type hierarchy] [the specified current type]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsInTypeHierarchy(Type currentType, Type checkType)
        {
            while (currentType != null && currentType != typeof(object))
            {
                if (currentType == checkType)
                {
                    return true;
                }

                currentType = currentType.BaseType;
            }

            return false;
        }

        #endregion ILog Members
    }
}