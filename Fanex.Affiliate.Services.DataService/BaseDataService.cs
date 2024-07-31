namespace Fanex.Affiliate.Services.DataService
{
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Caching;
    using Fanex.Caching.Memory;

    /// <summary>
    /// Base DataService.
    /// </summary>
    public abstract class BaseDataService
    {
        /// <summary>
        /// Initializes static members of the <see cref="BaseDataService"/> class.
        /// </summary>
        static BaseDataService()
        {
            Cache = new MemoryCache(new MemoryCacheOptions());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataService" /> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        protected BaseDataService(Licensee licensee)
        {
            LogFormatter = new LogFormatter();
            Licensee = licensee;
        }

        /// <summary>
        /// Gets a service will be associated with a Licensee.
        /// </summary>
        /// <value>
        /// The licensee object.
        /// </value>
        public Licensee Licensee { get; private set; }

        /// <summary>
        /// Gets or sets a service will have a LogTracer to write log.
        /// </summary>
        /// <value>
        /// The log tracer object.
        /// </value>
        public LogTracer LogTracer { get; protected set; }

        /// <summary>
        /// Gets a cache Service.
        /// </summary>
        /// <value>
        /// The cache object.
        /// </value>
        protected static ICache Cache { get; private set; }

        /// <summary>
        /// Gets a Log Formatter.
        /// </summary>
        /// <value>
        /// The log formatter.
        /// </value>
        protected LogFormatter LogFormatter { get; private set; }

        /// <summary>
        /// Sets the log tracer.
        /// </summary>
        /// <param name="logTracer">The log tracer.</param>
        public void SetLogTracer(LogTracer logTracer)
        {
            LogTracer = logTracer;
        }

        /// <summary>
        /// Gets the licensee cache key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>Returns cache license object.</returns>
        protected string GetLicenseeCacheKey(string key)
        {
            return string.Format("{0}_{1}", Licensee.Name, key);
        }
    }
}