namespace Fanex.Affiliate.Services.DataAccess
{
    using System;
    using System.Collections;
    using System.Data;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataAccess.Util;

    /// <summary>
    /// Import Exchange Rate DataAccess.
    /// </summary>
    public class ExchangeRateDataAccess : BaseDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeRateDataAccess"/> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        public ExchangeRateDataAccess(Licensee licensee)
            : base(licensee)
        {
        }

        /// <summary>
        /// Exchanges the daily selected by date.
        /// </summary>
        /// <param name="date">The selected date.</param>
        /// <returns>Returns Data table.</returns>
        public DataTable GetOandaExchangeDaily(DateTime date)
        {
            var param = new Hashtable { { "date", date } };
            return ExecSpDataTable("Common_ExchangeDailyOanda_SelByDate", param);
        }

        /// <summary>
        /// Import Exchange Rate.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="date">The import date.</param>
        /// <returns>Returns result.</returns>
        public Result ImportAffiliateDailyExchange(string xmlData, DateTime date)
        {
            var param = new Hashtable { { "date", date }, { "xml", xmlData }, { "errcode", 0 }, { "errmsg", string.Empty } };

            var result = ExecSp("Task_aff_DailyImportExchange", param);
            return result.ToResult();
        }
    }
}