namespace Fanex.Affiliate.Services.Jobs.Importer
{
    using System;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataService;
    using Fanex.Affiliate.Services.Jobs.DTO;
    using Fanex.Affiliate.Services.Jobs.Helpers;
    using NodaTime;

    /// <summary>
    ///     The controller to import member info.
    /// </summary>
    public class MemberInfoImporter : BaseImporter
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
        ///     Initializes a new instance of the <see cref="MemberInfoImporter" /> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        public MemberInfoImporter(Licensee licensee)
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
                if (UmService.IsDownTime)
                {
                    LogTracer.Append("Is Down Time");
                    LogHelper.LogUM();
                    return;
                }

                // import data
                DateTime serverTime = _commonService.DatabaseServerTime;
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
                if (UmService.IsDownTime)
                {
                    LogTracer.Append("Is Down Time");
                    LogHelper.LogUM();
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
            var message = $"{nameof(ImportMemberInfo)} {DateTime.Now}";
            LogHelper.InfoImport($"{_commonService.SystemName} starting {message}", date);

            string xmlData = _dataService.GetMemberInfo(date);
            if (xmlData != "<root />")
            {
                _dataService.ImportMemberInfo(xmlData, date);
            }

            LogHelper.InfoImport($"{_commonService.SystemName} finished {message}", date);
        }

        /// <summary>
        ///     Updates the first deposit date.
        /// </summary>
        private void UpdateFirstDepositDate()
        {
            string xmlData = _dataService.FirstDepositDateData;
            if (xmlData != "<root />")
            {
                _dataService.UpdateFirstDepositDate(xmlData);
            }
        }
    }
}