// --------------------------------------------------------------------------
// <copyright file="ImportType.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Jobs.DTO
{
    /// <summary>
    ///     Import Types.
    /// </summary>
    public enum ImportType
    {
        /// <summary>
        /// The none ENUM.
        /// </summary>
        None = 0,

        /// <summary>
        ///     The import exchange rate.
        /// </summary>
        ImportExchangeRate = 1,

        /// <summary>
        ///     The import member daily.
        /// </summary>
        ImportMemberDaily = 2,

        /// <summary>
        ///     The import member information.
        /// </summary>
        ImportMemberInfo = 3
    }
}