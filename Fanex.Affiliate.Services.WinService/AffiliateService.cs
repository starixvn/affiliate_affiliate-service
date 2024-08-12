namespace Fanex.Affiliate.Services.WinService
{
    using System;
    using System.ServiceProcess;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.Jobs.Helpers;
    using Fanex.Data.Configuration;
    using Quartz;
    using Quartz.Impl;

    /// <summary>
    /// Affiliate Service.
    /// </summary>
    public partial class AffiliateService : ServiceBase
    {
        /// <summary>
        /// Quartz ISchedulerFactory.
        /// </summary>
        private readonly ISchedulerFactory _schedulerFactory;

        /// <summary>
        /// Quartz IScheduler.
        /// </summary>
        private readonly IScheduler _scheduler;

        /// <summary>
        /// Initializes a new instance of the <see cref="AffiliateService"/> class.
        /// </summary>
        public AffiliateService()
        {
            try
            {
                InitializeComponent();

                ServiceName = Constants.ServiceName;

                FanexService.SetupSentryLogging();
                FanexService.SetUpUMEnviroment();
                DbSettingProviderManager.Current.Start();
                AppSetting.Initialize();
                LicenseeManager.Initialize();
                _schedulerFactory = new StdSchedulerFactory();
                _scheduler = _schedulerFactory.GetScheduler();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
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
            try
            {
                LogHelper.Info("Starting Athena services!");
                _scheduler.Start();
            }
            catch (Exception exception)
            {
                LogHelper.Error(exception);
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM).
        /// Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            LogHelper.Info("Stop Athena services!");
            _scheduler.Shutdown(true);
        }
    }
}