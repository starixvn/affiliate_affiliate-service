// --------------------------------------------------------------------------
// <copyright file="ImportMemberDailyJob.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Controller.QuartzJob
{
    using Fanex.Affiliate.Services.Controller.Implement;
    using Quartz;

    /// <summary>
    ///     A Quartz Job which call controller for import member daily.
    /// </summary>
    public class ImportMemberDailyJob : BaseJob, IJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportMemberDailyJob" /> class.
        /// </summary>
        public ImportMemberDailyJob()
        {
            ControllerType = typeof(ImportMemberDailyController);
        }

        /// <summary>
        ///     Executes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(IJobExecutionContext context)
        {
            Run(context);
        }
    }
}