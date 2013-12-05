// --------------------------------------------------------------------------
// <copyright file="MemberDailyDataService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataService.Implement
{
    using System;
    using System.Reflection;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataAccess;
    using Fanex.Affiliate.Services.DataService.Xml;

    /// <summary>
    /// Member Daily DataService.
    /// </summary>
    public class MemberDailyDataService : BaseDataService
    {
        /// <summary>
        /// The data access.
        /// </summary>
        private readonly MemberDailyDataAccess _dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberDailyDataService"/> class.
        /// </summary>
        /// <param name="licensee">The licensee information.</param>
        public MemberDailyDataService(Licensee licensee)
            : base(licensee)
        {
            _dataAccess = new MemberDailyDataAccess(licensee);
        }

        /// <summary>
        /// Gets the members.
        /// </summary>
        /// <param name="date">The selected date.</param>
        /// <returns>Returns list of members.</returns>
        public string GetMembers(DateTime date)
        {
            LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), date));
            var dataTable = _dataAccess.Aff_GetMembers(date);
            return XmlConverter.ToXmlString(dataTable);
        }

        /// <summary>
        /// Gets the players win lost by product.
        /// </summary>
        /// <param name="xmlMembers">The XML members.</param>
        /// <param name="usernamePrefix">The username prefix.</param>
        /// <param name="date">The selected date.</param>
        /// <returns>Returns list of players.</returns>
        public string GetPlayersWinlostByProduct(string xmlMembers, string usernamePrefix, DateTime date)
        {
            if (!string.IsNullOrEmpty(xmlMembers))
            {
                LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), xmlMembers, usernamePrefix, date));
                var dataTable = _dataAccess.Aff_PlayersWinlostByProductSel(xmlMembers, usernamePrefix, date);
                return XmlConverter.ToXmlString(dataTable);
            }

            return null;
        }

        /// <summary>
        /// Gets the players funding.
        /// </summary>
        /// <param name="xmlMembers">The XML Members.</param>
        /// <param name="usernamePrefix">The username prefix.</param>
        /// <param name="date">The selected date.</param>
        /// <returns>Returns list of players.</returns>
        public string GetPlayersFunding(string xmlMembers, string usernamePrefix, DateTime date)
        {
            if (!string.IsNullOrEmpty(xmlMembers))
            {
                LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), xmlMembers, usernamePrefix, date));
                var dataTable = _dataAccess.Aff_PlayersFundingSel(xmlMembers, usernamePrefix, date);
                return XmlConverter.ToXmlString(dataTable);
            }

            return null;
        }

        /// <summary>
        /// Imports the member daily.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="date">The import date.</param>
        /// <returns>Returns import result.</returns>
        public Result ImportMemberDaily(string xmlData, DateTime date)
        {
            if (!string.IsNullOrEmpty(xmlData))
            {
                LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), xmlData, date));
                return _dataAccess.Task_Aff_MemberDailyImportData(xmlData, date);
            }

            return Result.Empty;
        }

        /// <summary>
        /// Imports the member daily transaction.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="date">The import date.</param>
        /// <returns>Returns import result.</returns>
        public Result ImportMemberDailyTransaction(string xmlData, DateTime date)
        {
            if (!string.IsNullOrEmpty(xmlData))
            {
                LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), xmlData, date));
                return _dataAccess.Task_Aff_MemberDailyTransactionImportData(xmlData, date);
            }

            return Result.Empty;
        }
    }
}