using Fanex.Affiliate.Services.Core;
using Fanex.Affiliate.Services.Core.Configuration;
using Fanex.Data.Configuration;
using Quartz;
using Quartz.Impl;
using System;

namespace Fanex.Afiliate.Services.ConsoleApp
{
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
            DbSettingProviderManager.Current.Start();
            Fanex.Logging.LogService.Error("test");
            AppSetting.Initialize();
            LicenseeManager.Initialize();
            _schedulerFactory = new StdSchedulerFactory();
            _scheduler = _schedulerFactory.GetScheduler();
            _scheduler.Start();
           
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            (new JobInstance()).Run();
        }
    }
}