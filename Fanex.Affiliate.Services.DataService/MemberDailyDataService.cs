namespace Fanex.Affiliate.Services.DataService
{
    using System;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Text;
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
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "Reviewed.")]
        public string GetMembers(DateTime date, out string productIds)
        {
            productIds = null;
            LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), date, productIds));
            var dataSet = _dataAccess.GetAffiliateMembers(date);
            var memberXml = XmlConverter.ToXmlString(dataSet.Tables[0]);

            // Get product id list.
            var productIdBuilder = new StringBuilder();
            foreach (DataRow row in dataSet.Tables[1].Rows)
            {
                productIdBuilder.Append(row["ProductId"]).Append(",");
            }

            productIds = productIdBuilder.ToString().TrimEnd(',');
            return memberXml;
        }

        /// <summary>
        /// Gets the players win lost by product.
        /// </summary>
        /// <param name="xmlMembers">The XML members.</param>
        /// <param name="usernamePrefix">The username prefix.</param>
        /// <param name="date">The selected date.</param>
        /// <returns>Returns list of players.</returns>
        public string GetPlayersWinlostByProduct(string xmlMembers, string usernamePrefix, DateTime date, string productIds)
        {
            if (!string.IsNullOrEmpty(xmlMembers))
            {
                LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), xmlMembers, usernamePrefix, date, productIds));
                var dataTable = _dataAccess.GetAffiliatePlayersWinlostByProduct(xmlMembers, usernamePrefix, date, productIds);
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
                var dataTable = _dataAccess.GetAffiliatePlayersFunding(xmlMembers, usernamePrefix, date);
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
                return _dataAccess.ImportAffiliateMemberDaily(xmlData, date);
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
                return _dataAccess.ImportAffiliateMemberDailyTransaction(xmlData, date);
            }

            return Result.Empty;
        }
    }
}