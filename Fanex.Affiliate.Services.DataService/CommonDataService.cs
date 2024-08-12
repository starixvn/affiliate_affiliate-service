namespace Fanex.Affiliate.Services.DataService
{
    using System;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataAccess;
    using Fanex.Affiliate.Services.DataAccess.DTO;
    using Fanex.Caching;
    using Newtonsoft.Json;

    /// <summary>
    /// Common DataService.
    /// </summary>
    public class CommonDataService : BaseDataService
    {
        /// <summary>
        /// The data access.
        /// </summary>
        private readonly CommonDataAccess _dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonDataService"/> class.
        /// </summary>
        /// <param name="licensee">The licensee information.</param>
        public CommonDataService(Licensee licensee)
            : base(licensee)
        {
            _dataAccess = new CommonDataAccess(licensee);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonDataService"/> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        /// <param name="logTracer">The log tracer.</param>
        public CommonDataService(Licensee licensee, LogTracer logTracer)
            : this(licensee)
        {
            LogTracer = logTracer;
        }

        /// <summary>
        /// Gets the system name info.
        /// </summary>
        /// <value>Returns DTO object.</value>
        public SystemSettings SystemNameInfo
        {
            get
            {
                var cacheKey = GetLicenseeCacheKey("SystemName");
                var result = Get<SystemSettings>(cacheKey);
                if (result == null)
                {
                    result = _dataAccess.GetAffiliateSystemSettingsByName("SystemName");
                    Set(cacheKey, result, AppSetting.ExpiredCacheTime);
                }

                return result;
            }
        }

        public void Set<T>(string key, T value, int seconds)
        {
            var cachePolicy = new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(seconds));
            Cache.Set(key, DeepClone(value), cachePolicy);
        }

        private T Get<T>(string key)
        {
            if (Cache.TryGet<T>(key, out T value))
            {
                return DeepClone(value);
            }

            return value;
        }

        private static T DeepClone<T>(T source)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }

        /// <summary>
        /// Gets the name of the system.
        /// </summary>
        /// <value>Returns system name.</value>
        public string SystemName
        {
            get
            {
                return SystemNameInfo.SValue;
            }
        }

        /// <summary>
        /// Gets the database server time.
        /// </summary>
        /// <value>Return date time.</value>
        public DateTime DatabaseServerTime
        {
            get
            {
                return _dataAccess.AffiliateCurrentDate;
            }
        }
    }
}