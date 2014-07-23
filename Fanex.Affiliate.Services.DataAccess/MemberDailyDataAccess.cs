// --------------------------------------------------------------------------
// <copyright file="MemberDailyDataAccess.cs" company="Nexcel Solutions Vietnam">
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
    using System.Data;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataAccess.Util;

    /// <summary>
    /// Import Member Daily DataAccess.
    /// </summary>
    public class MemberDailyDataAccess : BaseDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberDailyDataAccess"/> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        public MemberDailyDataAccess(Licensee licensee)
            : base(licensee)
        {
        }

        /// <summary>
        /// Import member daily.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="date">The import date.</param>
        /// <returns>Returns result.</returns>
        public Result ImportAffiliateMemberDaily(string xmlData, DateTime date)
        {
            var param = new Hashtable
                {
                    { "adminid", 9999 },
                    { "xmldata", xmlData },
                    { "winlostdate", date },
                    { "errcode", 0 },
                    { "errmsg", string.Empty }
                };

            var result = ExecSp("Task_Aff_MemberDailyImportData", param);
            return result.ToResult();
        }

        /// <summary>
        /// Import member daily transaction.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="date">The import date.</param>
        /// <returns>Returns result.</returns>
        public Result ImportAffiliateMemberDailyTransaction(string xmlData, DateTime date)
        {
            var param = new Hashtable
                {
                    { "adminid", 9999 },
                    { "xmldata", xmlData },
                    { "winlostdate", date },
                    { "errcode", 0 },
                    { "errmsg", string.Empty }
                };

            var result = ExecSp("Task_Aff_MemberDailyTransactionImportData", param);
            return result.ToResult();
        }

        /// <summary>
        /// Get members.
        /// </summary>
        /// <param name="date">The selected date.</param>
        /// <returns>Returns Data table.</returns>
        public DataTable GetAffiliateMembers(DateTime date)
        {
            var param = new Hashtable { { "date", date } };
            return ExecSpDataTable("Aff_GetMembers", param);
        }

        /// <summary>
        /// Get Player has Win lost.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="usernamePrefix">The username prefix.</param>
        /// <param name="date">The selected date.</param>
        /// <returns>Returns data table.</returns>
        public DataTable GetAffiliatePlayersWinlostByProduct(string xmlData, string usernamePrefix, DateTime date)
        {
            var param = new Hashtable { { "xml", xmlData }, { "prefix", usernamePrefix }, { "date", date } };
            return ExecSpDataTable("Common_Aff_PlayersWinlostByProductSel", param);
        }

        /// <summary>
        /// Get Player has Funding.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="usernamePrefix">The username prefix.</param>
        /// <param name="date">The selected date.</param>
        /// <returns>Returns data table.</returns>
        public DataTable GetAffiliatePlayersFunding(string xmlData, string usernamePrefix, DateTime date)
        {
            var param = new Hashtable { { "xml", xmlData }, { "prefix", usernamePrefix }, { "date", date } };
            return ExecSpDataTable("Common_Aff_PlayersFundingSel", param);
        }
    }
}