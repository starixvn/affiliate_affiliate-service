namespace Fanex.Affiliate.Services.Jobs.Importer
{
    using System;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataService;
    using Fanex.Affiliate.Services.Jobs.DTO;
    using NodaTime;

    /// <summary>
    ///     The controller to import exchange rate.
    /// </summary>
    public class ExchangeRateImporter : BaseImporter
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
        private readonly ExchangeRateDataService _dataService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExchangeRateImporter" /> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        public ExchangeRateImporter(Licensee licensee)
        {
            _dataService = _dataService ?? CreateService<ExchangeRateDataService>(licensee);
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

                DateTime serverTime = _commonService.DatabaseServerTime;
                DateTime serverDate = serverTime.Date;
                var localTime = new LocalTime(serverTime.Hour, serverTime.Minute, serverTime.Second);
                if (serverDate != _lastImportedDate && localTime >= AppSetting.ImportExchangeRateStdTime)
                {
                    // refresh data for yesterday (only one time)
                    _lastImportedDate = serverDate;
                    DateTime yesterday = serverDate.AddDays(-1);
                    ImportExchangeRate(yesterday);
                }
                else
                {
                    ImportExchangeRate(serverDate);
                }
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
                    ImportExchangeRate(date);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                throw;
            }
        }

        /// <summary>
        ///     Imports the exchange rate.
        /// </summary>
        /// <param name="date">The import date.</param>
        private void ImportExchangeRate(DateTime date)
        {
            LogHelper.InfoImport($"{_commonService.SystemName} starting {nameof(ImportExchangeRate)}", date);

            string xmlData = _dataService.GetExchangeDailyOanda(date);
            if (xmlData != "<root />")
            {
                _dataService.ImportExchangeRate(xmlData, date);
            }

            LogHelper.InfoImport($"{_commonService.SystemName} finished {nameof(ImportExchangeRate)}", date);
        }
    }
}