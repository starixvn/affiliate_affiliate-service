// --------------------------------------------------------------------------
// <copyright file="LoggingMode.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

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