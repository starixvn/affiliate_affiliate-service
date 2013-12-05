// --------------------------------------------------------------------------
// <copyright file="ImportMemberDailyController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Controller.Implement
{
    using System;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataService.Implement;

    /// <summary>
    ///     The controller to import member daily.
    /// </summary>
    public class ImportMemberDailyController : ImportController
    {
        /// <summary>
        ///     The common service.
        /// </summary>
        private readonly CommonDataService _commonService;

        /// <summary>
        ///     The data service.
        /// </summary>
        private readonly MemberDailyDataService _dataService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImportMemberDailyController" /> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        public ImportMemberDailyController(Licensee licensee)
        {
            _dataService = _dataService ?? CreateService<MemberDailyDataService>(licensee);
            _commonService = _commonService ?? CreateService<CommonDataService>(licensee);
        }

        /// <summary>
        ///     Run controller's business.
        /// </summary>
        /// <param name="importInfo">The import information.</param>
        protected override void Run(ImportInfo importInfo)
        {
            try
            {
                if (AppService.IsDownTime)
                {
                    LogTracer.Append("Is Down Time");
                    return;
                }

                DateTime serverTime = _commonService.GetDbServerTime();

                //// import today
                DateTime serverDate = serverTime.Date;
                ImportMemberDaily(serverDate);

                //// import yesterday
                DateTime yesterday = serverDate.AddDays(-1);
                ImportMemberDaily(yesterday);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        /// <summary>
        ///     Run import in win form.
        /// </summary>
        /// <param name="importInfo">The import information.</param>
        protected override void Run(ManuallyImportInfo importInfo)
        {
            try
            {
                if (AppService.IsDownTime)
                {
                    LogTracer.Append("Is Down Time");
                    return;
                }

                int totalDays = importInfo.ToDate.Subtract(importInfo.FromDate).Days + 1;
                for (int i = 0; i < totalDays; i++)
                {
                    DateTime date = importInfo.FromDate.AddDays(i);
                    ImportMemberDaily(date);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                throw;
            }
        }

        /// <summary>
        ///     Imports the member daily.
        /// </summary>
        /// <param name="date">The import date.</param>
        private void ImportMemberDaily(DateTime date)
        {
            string xmlMembers = _dataService.GetMembers(date);
            string usernamePrefix = _dataService.Licensee.UsernamePrefix;

            // ImportMemberDaily
            string xmlPlayersWinlost = _dataService.GetPlayersWinlostByProduct(xmlMembers, usernamePrefix, date);
            _dataService.ImportMemberDaily(xmlPlayersWinlost, date);

            // ImportMemberDailyTransaction
            string xmlPlayersFunding = _dataService.GetPlayersFunding(xmlMembers, usernamePrefix, date);
            _dataService.ImportMemberDailyTransaction(xmlPlayersFunding, date);
        }
    }
}