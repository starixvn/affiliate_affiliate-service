// --------------------------------------------------------------------------
// <copyright file="Licensee.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Core.Configuration
{
    using System;
    using System.Linq;

    /// <summary>
    /// Licensee information.
    /// </summary>
    public partial class Licensee
    {
        /// <summary>
        /// Gets Licensee username prefix.
        /// </summary>
        /// <value>
        /// The username prefix.
        /// </value>
        public string UsernamePrefix
        {
            get
            {
                var setting = Settings.FirstOrDefault(x => x.Key.Equals("UsernamePrefix", StringComparison.CurrentCultureIgnoreCase));
                return setting != null ? setting.Value : null;
            }
        }
    }
}