namespace Fanex.Affiliate.Services.Jobs
{
    using System;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Jobs.DTO;
    using Quartz;
    using Quartz.Impl.Triggers;

    /// <summary>
    ///     A abstract Quartz job.
    /// </summary>
    public abstract class BaseJob
    {
        /// <summary>
        /// Gets or sets the controller will be called by this job.
        /// </summary>
        /// <value>The type of controller.</value>
        public Type ControllerType { get; set; }

        /// <summary>
        ///     Gets the import info from Quartz context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Returns the import information.</returns>
        /// <exception cref="System.Exception">Error to get ImportInfo.</exception>
        protected ImportInfo GetImportInfo(IJobExecutionContext context)
        {
            var trigger = (AbstractTrigger)context.Trigger;
            var licenseeName = trigger.Group;
            var licensee = LicenseeManager.GetLicensee(licenseeName);

            var importInfo = new ImportInfo
            {
                Licensee = licensee
            };

            if (importInfo.IsValid())
            {
                return importInfo;
            }

            throw new ArgumentException("Error to get ImportInfo");
        }

        /// <summary>
        ///     Called to run controller from Quartz job implements.
        /// </summary>
        /// <param name="context">The context.</param>
        protected void Run(IJobExecutionContext context)
        {
            var importInfo = GetImportInfo(context);
            var importController = (BaseImporter)Activator.CreateInstance(ControllerType, importInfo.Licensee);
            importController.Import(importInfo);
        }
    }
}