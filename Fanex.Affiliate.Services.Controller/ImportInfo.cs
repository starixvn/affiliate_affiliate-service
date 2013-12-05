// --------------------------------------------------------------------------
// <copyright file="ImportInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Controller
{
    using Fanex.Affiliate.Services.Core.Configuration;

    /// <summary>
    ///     The information used for importing for both win form and win service.
    /// </summary>
    public class ImportInfo
    {
        /// <summary>
        ///     Gets or sets the licensee.
        /// </summary>
        /// <value>
        ///     The licensee.
        /// </value>
        public Licensee Licensee { get; set; }

        /// <summary>
        ///     Gets or sets the error message.
        /// </summary>
        /// <value>
        ///     The error message.
        /// </value>
        public string ErrorMessage { get; protected set; }

        /// <summary>
        ///     Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsValid()
        {
            ErrorMessage = string.Empty;

            if (Licensee == null)
            {
                ErrorMessage = "Licensee must be not null";
                return false;
            }

            return true;
        }
    }
}