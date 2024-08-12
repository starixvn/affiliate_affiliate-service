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
    using Quartz;
    using Quartz.Impl;

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
            FanexService.SetupSentryLogging();
            FanexService.SetUpUMEnviroment();
            DbSettingProviderManager.Current.Start();
            AppSetting.Initialize();
            LicenseeManager.Initialize();
            _schedulerFactory = new StdSchedulerFactory();
            _scheduler = _schedulerFactory.GetScheduler();
            _scheduler.Start();
        }
    }
}
