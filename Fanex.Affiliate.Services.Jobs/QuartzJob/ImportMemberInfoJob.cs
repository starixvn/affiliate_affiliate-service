namespace Fanex.Affiliate.Services.Jobs.QuartzJob
{
    using Fanex.Affiliate.Services.Jobs;
    using Fanex.Affiliate.Services.Jobs.Importer;
    using Quartz;

    /// <summary>
    ///     A Quartz Job which call controller for import member info.
    /// </summary>
    public class ImportMemberInfoJob : BaseJob, IJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportMemberInfoJob" /> class.
        /// </summary>
        public ImportMemberInfoJob()
        {
            ControllerType = typeof(MemberInfoImporter);
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