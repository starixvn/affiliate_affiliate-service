namespace Fanex.Affiliate.Services.WinService
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.ServiceProcess;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Data.Configuration;
    using Fanex.Logging;
    using Fanex.Logging.Sentry.RavenClient;
    using Quartz;
    using Quartz.Impl;
    using SharpRaven;

    /// <summary>
    /// Affiliate Service.
    /// </summary>
    public partial class AffiliateService : ServiceBase
    {
        /// <summary>
        /// Quartz ISchedulerFactory.
        /// </summary>
        private ISchedulerFactory _schedulerFactory;

        /// <summary>
        /// Quartz IScheduler.
        /// </summary>
        private IScheduler _scheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="AffiliateService"/> class.
        /// </summary>
        public AffiliateService()
        {
            try
            {
                InitializeComponent();

                ServiceName = Constants.ServiceName;

                SetupSentryLogging();
                DbSettingProviderManager.Current.Start();
                AppSetting.Initialize();
                LicenseeManager.Initialize();
                _schedulerFactory = new StdSchedulerFactory();
                _scheduler = _schedulerFactory.GetScheduler();
            }
            catch (Exception ex)
            {
                Logging.LogService.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or
        /// when the operating system starts (for a service that starts automatically).
        /// Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            _scheduler.Start();

            Logging.LogService.Info("Importing data service started");
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM).
        /// Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            _scheduler.Shutdown(true);
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