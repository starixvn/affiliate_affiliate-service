using Fanex.Affiliate.Services.Core;
using Fanex.Affiliate.Services.Core.Configuration;
using Fanex.Data.Configuration;
using Quartz;
using Quartz.Impl;
namespace Fanex.Afiliate.Services.ConsoleApp
{
    class Program
    {
        /// <summary>
        /// Quartz ISchedulerFactory.
        /// </summary>
        private static ISchedulerFactory _schedulerFactory;

        /// <summary>
        /// Quartz IScheduler.
        /// </summary>
        private static IScheduler _scheduler;

        static void Main(string[] args)
        {

            DbSettingProviderManager.Current.Start();
            AppSetting.Initialize();
            LicenseeManager.Initialize();
            _schedulerFactory = new StdSchedulerFactory();
            _scheduler = _schedulerFactory.GetScheduler();
            _scheduler.Start();
        }
    }
}
