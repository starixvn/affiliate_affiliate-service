// -----------------------------------------------------------------------
// <copyright file="JobInstance.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Afiliate.Services.ConsoleApp
{
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Data.Configuration;
    using Fanex.Logging;
    using Fanex.Logging.Sentry.RavenClient;
    using Quartz;
    using Quartz.Impl;
    using SharpRaven;
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class JobInstance
    {
        /// <summary>
        /// Quartz ISchedulerFactory.
        /// </summary>
        private ISchedulerFactory _schedulerFactory;

        /// <summary>
        /// Quartz IScheduler.
        /// </summary>
        private IScheduler _scheduler;

        public void Run()
        {
            SetupSentryLogging();
            DbSettingProviderManager.Current.Start();
            AppSetting.Initialize();
            LicenseeManager.Initialize();
            _schedulerFactory = new StdSchedulerFactory();
            _scheduler = _schedulerFactory.GetScheduler();
            _scheduler.Start();
        }

        private static void SetupSentryLogging()
        {
            var appSetting = ConfigurationManager.AppSettings;

            var env = appSetting.Get("Environment");
            var backEndDsn = appSetting.Get("SentryBackEndDsn");
            var category = appSetting.Get("LoggingCategory");

            if (!string.IsNullOrEmpty(backEndDsn))
            {
                LogManager.SetDefaultLogCategory(category).Use(new List<ILogEngine>
                {
                    new SentryLogging(new SentryEngineOptions
                    {
                        Dsn = new Dsn(backEndDsn),
                        Environment = env
                    })
                });
            }
        }
    }
}
