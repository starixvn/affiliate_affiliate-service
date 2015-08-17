// --------------------------------------------------------------------------
// <copyright file="ImportExchangeRateJob.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Jobs.QuartzJob
{
    using Fanex.Affiliate.Services.Jobs;
    using Fanex.Affiliate.Services.Jobs.Importer;
    using Quartz;

    /// <summary>
    ///     A Quartz Job which call controller for import exchange rate.
    /// </summary>
    public class ImportExchangeRateJob : BaseJob, IJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportExchangeRateJob" /> class.
        /// </summary>
        public ImportExchangeRateJob()
        {
            ControllerType = typeof(ExchangeRateImporter);
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