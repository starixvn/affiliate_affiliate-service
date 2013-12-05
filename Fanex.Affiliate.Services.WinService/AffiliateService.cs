// --------------------------------------------------------------------------
// <copyright file="AffiliateService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.WinService
{
    using System;
    using System.Diagnostics;
    using System.ServiceProcess;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.Core.Util;
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
            InitializeComponent();

            try
            {
                ServiceName = Constants.ServiceName;

                DbSettingProviderManager.Current.Start();
                AppSetting.Initialize();
                LicenseeManager.Initialize();
                _schedulerFactory = new StdSchedulerFactory();
                _scheduler = _schedulerFactory.GetScheduler();
            }
            catch (Exception ex)
            {
                string message = ExceptionFormatter.GetMessage(ex);
                EventLog.WriteEntry(Constants.ServiceName, message, EventLogEntryType.Error);
                throw;
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
                _scheduler.Start();
            }
            catch (Exception ex)
            {
                string message = ExceptionFormatter.GetMessage(ex);
                EventLog.WriteEntry(Constants.ServiceName, message, EventLogEntryType.Error);
                throw;
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM).
        /// Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                _scheduler.Shutdown(true);
            }
            catch (Exception ex)
            {
                string message = ExceptionFormatter.GetMessage(ex);
                EventLog.WriteEntry(Constants.ServiceName, message, EventLogEntryType.Error);
            }

            GC.Collect();
        }
    }
}