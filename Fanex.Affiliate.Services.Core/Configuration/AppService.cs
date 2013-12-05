// --------------------------------------------------------------------------
// <copyright file="AppService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Core.Configuration
{
    using Sunplus.UMControllerLibrary;

    /// <summary>
    /// Application Service.
    /// </summary>
    public static class AppService
    {
        /// <summary>
        /// Gets a value indicating whether now is down time.
        /// </summary>
        /// <value>
        ///   <c>true</c> if is down time, otherwise <c>false</c>.
        /// </value>
        public static bool IsDownTime
        {
            get
            {
                var um = new UMBusiness();
                return um.isUnderMaintainence(Sites.Affiliate, AppSetting.LbUrl);
            }
        }
    }
}