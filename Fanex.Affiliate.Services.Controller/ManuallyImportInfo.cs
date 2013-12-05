// --------------------------------------------------------------------------
// <copyright file="ManuallyImportInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Controller
{
    using System;
    using Fanex.Affiliate.Services.Core.Configuration;

    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    public class ManuallyImportInfo : ImportInfo
    {
        /// <summary>
        ///     Gets or sets from date.
        /// </summary>
        /// <value>
        ///     From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        ///     Gets or sets the automatic date.
        /// </summary>
        /// <value>
        ///     The automatic date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        ///     Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid()
        {
            if (FromDate > ToDate)
            {
                return false;
            }

            return base.IsValid();
        }
    }
}