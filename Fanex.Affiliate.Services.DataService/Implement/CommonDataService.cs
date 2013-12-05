// --------------------------------------------------------------------------
// <copyright file="CommonDataService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataService.Implement
{
    using System;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataAccess.DTO;
    using Fanex.Affiliate.Services.DataAccess.Implement;

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
        /// <returns>Returns DTO object.</returns>
        public Aff_SystemSettingsSelByName GetSystemNameInfo()
        {
            var cacheKey = GetLicenseeCacheKey("SystemName");
            var result = Cache.Get<Aff_SystemSettingsSelByName>(cacheKey);
            if (result == null)
            {
                result = _dataAccess.Aff_SystemSettingsSelByName("SystemName");
                Cache.Set(cacheKey, result, TimeSpan.FromSeconds(AppSetting.ExpiredCacheTime));
            }

            return result;
        }

        /// <summary>
        /// Gets the name of the system.
        /// </summary>
        /// <returns>Returns system name.</returns>
        public string GetSystemName()
        {
            return GetSystemNameInfo().SValue;
        }

        /// <summary>
        /// Gets the database server time.
        /// </summary>
        /// <returns>Return date time.</returns>
        public DateTime GetDbServerTime()
        {
            return _dataAccess.Aff_CurrentDate();
        }
    }
}