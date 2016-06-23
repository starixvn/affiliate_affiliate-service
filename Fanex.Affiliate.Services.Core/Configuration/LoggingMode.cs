namespace Fanex.Affiliate.Services.Core.Configuration
{
    /// <summary>
    /// Application Logging Modes.
    /// </summary>
    public enum LoggingMode
    {
        /// <summary>
        /// No logging.
        /// </summary>
        Disable = 0,

        /// <summary>
        /// Logging everything.
        /// </summary>
        All = 1,

        /// <summary>
        /// Logging only errors, except information.
        /// </summary>
        Error = -1
    }
}