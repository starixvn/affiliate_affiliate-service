// --------------------------------------------------------------------------
// <copyright file="ExchangeRateDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataAccess.Implement
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
        public DataTable ExchangeDailyOanda_SelByDate(DateTime date)
        {
            try
            {
                var param = new Hashtable { { "date", date } };
                return ExecSpDataTable("Common_ExchangeDailyOanda_SelByDate", param);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return null;
        }

        /// <summary>
        /// Import Exchange Rate.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="date">The import date.</param>
        /// <returns>Returns result.</returns>
        public Result Task_aff_DailyImportExchange(string xmlData, DateTime date)
        {
            try
            {
                var param = new Hashtable { { "date", date }, { "xml", xmlData }, { "errcode", 0 }, { "errmsg", string.Empty } };

                var result = ExecSp("Task_aff_DailyImportExchange", param);
                return result.ToResult();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return Result.Empty;
        }
    }
}