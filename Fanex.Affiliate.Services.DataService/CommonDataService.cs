// --------------------------------------------------------------------------
// <copyright file="CommonDataService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataService
{
    using System;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataAccess;
    using Fanex.Affiliate.Services.DataAccess.DTO;

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
                var result = Cache.Get<SystemSettings>(cacheKey);
                if (result == null)
                {
                    result = _dataAccess.GetAffiliateSystemSettingsByName("SystemName");
                    Cache.Set(cacheKey, result, TimeSpan.FromSeconds(AppSetting.ExpiredCacheTime));
                }

                return result;
            }
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