// --------------------------------------------------------------------------
// <copyright file="ImportMemberInfoController.cs" company="Nexcel Solutions Vietnam">
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
    using NodaTime;

    /// <summary>
    ///     The controller to import member info.
    /// </summary>
    public class ImportMemberInfoController : ImportController
    {
        /// <summary>
        ///     The last imported date.
        /// </summary>
        private static DateTime _lastImportedDate = DateTime.MinValue.Date;

        /// <summary>
        ///     The common service.
        /// </summary>
        private readonly CommonDataService _commonService;

        /// <summary>
        ///     The data service.
        /// </summary>
        private readonly MemberInfoDataService _dataService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImportMemberInfoController" /> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        public ImportMemberInfoController(Licensee licensee)
        {
            _dataService = _dataService ?? CreateService<MemberInfoDataService>(licensee);
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

                // import data
                DateTime serverTime = _commonService.GetDbServerTime();
                DateTime serverDate = serverTime.Date;
                var localTime = new LocalTime(serverTime.Hour, serverTime.Minute, serverTime.Second);
                if (serverDate != _lastImportedDate && localTime >= AppSetting.ImportMemberInfoStdTime)
                {
                    // refresh data for yesterday (only one time)
                    _lastImportedDate = serverDate;
                    DateTime yesterday = serverDate.AddDays(-1);
                    ImportMemberInfo(yesterday);
                }
                else
                {
                    ImportMemberInfo(serverDate);
                }

                // update data
                UpdateFirstDepositDate();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        /// <summary>
        ///     Runs the specified import info.
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

                // import data
                int totalDays = importInfo.ToDate.Subtract(importInfo.FromDate).Days + 1;
                for (int i = 0; i < totalDays; i++)
                {
                    DateTime date = importInfo.FromDate.AddDays(i);
                    ImportMemberInfo(date);
                }

                // update data
                UpdateFirstDepositDate();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                throw;
            }
        }

        /// <summary>
        ///     Imports the member info.
        /// </summary>
        /// <param name="date">The import date.</param>
        private void ImportMemberInfo(DateTime date)
        {
            string xmlData = _dataService.GetMemberInfo(date);
            if (xmlData != "<root />")
            {
                _dataService.ImportMemberInfo(xmlData, date);
            }
        }

        /// <summary>
        ///     Updates the first deposit date.
        /// </summary>
        private void UpdateFirstDepositDate()
        {
            string xmlData = _dataService.GetFirstDepositDateData();
            if (xmlData != "<root />")
            {
                _dataService.UpdateFirstDepositDate(xmlData);
            }
        }
    }
}