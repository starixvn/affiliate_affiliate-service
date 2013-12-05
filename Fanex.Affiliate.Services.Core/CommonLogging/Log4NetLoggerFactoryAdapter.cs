// --------------------------------------------------------------------------
// <copyright file="Log4NetLoggerFactoryAdapter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Core.CommonLogging
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using Common.Logging;
    using Common.Logging.Configuration;
    using Common.Logging.Factory;
    using log4net.Config;

    /// <summary>
    /// Implement Common.Logging.
    /// </summary>
    public class Log4NetLoggerFactoryAdapter : AbstractCachingLoggerFactoryAdapter
    {
        /// <summary>
        /// The runtime.
        /// </summary>
        private readonly ILog4NetRuntime _runtime;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetLoggerFactoryAdapter"/> class.
        /// </summary>
        /// <param name="properties">The name value collection of properties.</param>
        public Log4NetLoggerFactoryAdapter(NameValueCollection properties)
            : this(properties, new Log4NetRuntime())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetLoggerFactoryAdapter"/> class.
        /// </summary>
        /// <param name="properties">Configuration properties, see <see cref="Log4NetLoggerFactoryAdapter"/> for more.</param>
        /// <param name="runtime">A log4net runtime adapter.</param>
        protected Log4NetLoggerFactoryAdapter(NameValueCollection properties, ILog4NetRuntime runtime)
            : base(true)
        {
            if (runtime == null)
            {
                throw new ArgumentNullException("runtime");
            }

            _runtime = runtime;
            //// parse config properties
            string configType = ArgUtils.GetValue(properties, "configType", string.Empty).ToUpper();
            string configFile = ArgUtils.GetValue(properties, "configFile", string.Empty);

            //// app-relative path?
            if (configFile.StartsWith("~/") || configFile.StartsWith("~\\"))
            {
                configFile = string.Format("{0}/{1}", AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\'), configFile.Substring(2));
            }

            if (configType == "FILE" || configType == "FILE-WATCH")
            {
                if (configFile == string.Empty)
                {
                    const string ExceptionMsg = "Configuration property 'configFile' must be set for log4Net configuration of type 'FILE' or 'FILE-WATCH'.";
                    throw new ConfigurationException(ExceptionMsg);
                }

                if (!File.Exists(configFile))
                {
                    throw new ConfigurationException("log4net configuration file '" + configFile + "' does not exists");
                }
            }

            ExecuteLoggerRuntime(configType, configFile);
        }

        #region ILog4NetRuntime Interface

        /// <summary>
        /// Abstract interface to the underlying log4net runtime.
        /// </summary>
        public interface ILog4NetRuntime
        {
            /// <summary>Calls <see cref="XmlConfigurator.Configure()"/></summary>
            void XmlConfiguratorConfigure();

            /// <summary>
            /// Calls <see cref="XmlConfigurator.Configure(System.IO.FileInfo)" />
            /// </summary>
            /// <param name="configFile">The configuration file path.</param>
            void XmlConfiguratorConfigure(string configFile);

            /// <summary>
            /// Calls <see cref="XmlConfigurator.ConfigureAndWatch(System.IO.FileInfo)" />
            /// </summary>
            /// <param name="configFile">The configuration file path.</param>
            void XmlConfiguratorConfigureAndWatch(string configFile);

            /// <summary>Calls <see cref="BasicConfigurator.Configure()"/></summary>
            void BasicConfiguratorConfigure();

            /// <summary>
            /// Calls <see cref="LogManager.GetLogger(string)" />
            /// </summary>
            /// <param name="name">The logger name.</param>
            /// <returns>Returns ILog.</returns>
            log4net.ILog GetLogger(string name);
        }

        #endregion ILog4NetRuntime Interface

        /// <summary>
        /// Create a ILog instance by name.
        /// </summary>
        /// <param name="name">The logger name.</param>
        /// <returns>Returns ILog.</returns>
        protected override ILog CreateLogger(string name)
        {
            return new Log4NetLogger(_runtime.GetLogger(name));
        }

        /// <summary>
        /// Initialize the log4net configuration.
        /// </summary>
        /// <param name="configType">Configuration type.</param>
        /// <param name="configFile">Configuration file.</param>
        private void ExecuteLoggerRuntime(string configType, string configFile)
        {
            switch (configType)
            {
                case "INLINE":
                    _runtime.XmlConfiguratorConfigure();
                    break;

                case "FILE":
                    _runtime.XmlConfiguratorConfigure(configFile);
                    break;

                case "FILE-WATCH":
                    _runtime.XmlConfiguratorConfigureAndWatch(configFile);
                    break;

                case "EXTERNAL":
                    // Log4net will be configured outside of Common.Logging
                    break;

                default:
                    _runtime.BasicConfiguratorConfigure();
                    break;
            }
        }

        #region Log4NetRuntime class

        /// <summary>
        /// Log4Net Runtime.
        /// </summary>
        private class Log4NetRuntime : ILog4NetRuntime
        {
            /// <summary>
            /// Calls <see cref="XmlConfigurator.Configure()" />
            /// </summary>
            public void XmlConfiguratorConfigure()
            {
                if (!log4net.LogManager.GetRepository().Configured)
                {
                    XmlConfigurator.Configure();
                }
            }

            /// <summary>
            /// Calls <see cref="XmlConfigurator.Configure(System.IO.FileInfo)" />
            /// </summary>
            /// <param name="configFile">The file path.</param>
            public void XmlConfiguratorConfigure(string configFile)
            {
                XmlConfigurator.Configure(new FileInfo(configFile));
            }

            /// <summary>
            /// Calls <see cref="XmlConfigurator.ConfigureAndWatch(System.IO.FileInfo)" />
            /// </summary>
            /// <param name="configFile">The file path.</param>
            public void XmlConfiguratorConfigureAndWatch(string configFile)
            {
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
            }

            /// <summary>
            /// Calls <see cref="BasicConfigurator.Configure()" />
            /// </summary>
            public void BasicConfiguratorConfigure()
            {
                BasicConfigurator.Configure();
            }

            /// <summary>
            /// Calls <see cref="LogManager.GetLogger(string)" />
            /// </summary>
            /// <param name="name">The log name.</param>
            /// <returns>Returns a ILog.</returns>
            public log4net.ILog GetLogger(string name)
            {
                return log4net.LogManager.GetLogger(name);
            }
        }

        #endregion Log4NetRuntime class
    }
}