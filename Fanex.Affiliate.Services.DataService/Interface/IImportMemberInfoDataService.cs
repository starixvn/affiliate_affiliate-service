// --------------------------------------------------------------------------
// <copyright file="IImportMemberInfoDataService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataService.Interface
{
    using System;

    public interface IImportMemberInfoDataService
    {
        /// <summary>
        /// Gets the member info.
        /// </summary>
        /// <param name="date">The selected date.</param>
        /// <returns>Return member information.</returns>
        string GetMemberInfo(DateTime date);
    }
}