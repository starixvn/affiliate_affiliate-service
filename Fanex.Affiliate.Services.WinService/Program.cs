namespace Fanex.Affiliate.Services.WinService
{
    using System.ServiceProcess;

    /// <summary>
    /// Main entry point.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            var _servicesToRun = new ServiceBase[] { new AffiliateService() };
            ServiceBase.Run(_servicesToRun);
        }
    }
}