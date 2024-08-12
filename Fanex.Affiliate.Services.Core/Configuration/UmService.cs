using ONEbook.UM;
using ONEbook.UM.Services;

namespace Fanex.Affiliate.Services.Core.Configuration
{
    /// <summary>
    /// Application Service.
    /// </summary>
    public class UmService
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="UmService"/> class from being created.
        /// </summary>
        private UmService()
        {
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>Returns UmService object.</returns>
        public static UmService Create()
        {
            return new UmService();
        }

        /// <summary>
        /// Gets a value indicating whether now is down time.
        /// </summary>
        /// <value>
        ///   <c>true</c> if is down time, otherwise <c>false</c>.
        /// </value>
        public bool IsDownTime
        {
            get
            {
                var maintenanceService = new MaintenanceService(
                                      UMClientManager.RestClient,
                                      UMClientManager.CacheService,
                                      UMClientManager.UMClientConfigs,
                                      UMClientManager.HttpContext);

                return maintenanceService.IsUnderMaintenance();
            }
        }
    }
}