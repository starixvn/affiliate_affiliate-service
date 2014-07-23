// --------------------------------------------------------------------------
// <copyright file="MemberInfoDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataAccess.Util;

    /// <summary>
    /// Import Member Information DataAccess.
    /// </summary>
    public class MemberInfoDataAccess : BaseDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInfoDataAccess"/> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        public MemberInfoDataAccess(Licensee licensee)
            : base(licensee)
        {
        }

        /// <summary>
        /// Gets Missing Created Dates.
        /// </summary>
        /// <value>Returns a list of DTO object.</value>
        public IEnumerable<string> AffiliateMembersWhichMissCreatedDate
        {
            get
            {
                using (var reader = ExecSpDataReader("Aff_MemberInfoGetMissingCreatedDate"))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToString("username");
                    }
                }
            }
        }

        /// <summary>
        /// Import member information.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="registeredDate">The registered date.</param>
        /// <returns>Returns result.</returns>
        public Result ImportAffiliateMemberInfo(string xmlData, DateTime registeredDate)
        {
            var param = new Hashtable
                {
                    { "adminid", AppSetting.AdminId },
                    { "xmldata", xmlData },
                    { "registereddate", registeredDate },
                    { "errcode", 0 },
                    { "errmsg", string.Empty }
                };

            var result = ExecSp("Task_Aff_MemberInfoImportData", param);
            return result.ToResult();
        }

        /// <summary>
        /// Update member info.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>Returns result.</returns>
        public Result UpdateAffiliateMemberCreatedDate(string xmlData)
        {
            var param = new Hashtable { { "xmldata", xmlData }, { "errcode", 0 }, { "errmsg", string.Empty } };

            var result = ExecSp("Task_Aff_MemberInfoUpdateCreatedDate", param);
            return result.ToResult();
        }
    }
}