namespace Fanex.Affiliate.Services.DataService
{
    using System;
    using System.Reflection;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataAccess;
    using Fanex.Affiliate.Services.DataService.Xml;

    /// <summary>
    /// Exchange Rate DataService.
    /// </summary>
    public class ExchangeRateDataService : BaseDataService
    {
        /// <summary>
        /// The data access.
        /// </summary>
        private readonly ExchangeRateDataAccess _dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeRateDataService"/> class.
        /// </summary>
        /// <param name="licensee">The licensee information.</param>
        public ExchangeRateDataService(Licensee licensee)
            : base(licensee)
        {
            _dataAccess = new ExchangeRateDataAccess(licensee);
        }

        /// <summary>
        /// Gets the exchange daily OANDA.
        /// </summary>
        /// <param name="date">The import date.</param>
        /// <returns>Returns exchange daily OANDA.</returns>
        public string GetExchangeDailyOanda(DateTime date)
        {
            LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), date));
            var dataTable = _dataAccess.GetOandaExchangeDaily(date);
            return XmlConverter.ToXmlString(dataTable);
        }

        /// <summary>
        /// Imports the exchange rate.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="date">The import date.</param>
        /// <returns>Returns import result.</returns>
        public Result ImportExchangeRate(string xmlData, DateTime date)
        {
            if (!string.IsNullOrEmpty(xmlData))
            {
                LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), xmlData, date));
                return _dataAccess.ImportAffiliateDailyExchange(xmlData, date);
            }

            return Result.Empty;
        }
    }
}