namespace Fanex.Affiliate.Services.DataService
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.Core.Util;
    using Fanex.Affiliate.Services.DataAccess;
    using Fanex.Affiliate.Services.DataService.Xml;
    using Fanex.Affiliate.Services.DataService.Xml.Objects;

    /// <summary>
    /// Member Info Data Service.
    /// </summary>
    public class MemberInfoDataService : BaseDataService, IImportMemberInfoDataService
    {
        /// <summary>
        /// The data access.
        /// </summary>
        private readonly MemberInfoDataAccess _dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInfoDataService"/> class.
        /// </summary>
        /// <param name="licensee">The licensee information.</param>
        public MemberInfoDataService(Licensee licensee)
            : base(licensee)
        {
            _dataAccess = new MemberInfoDataAccess(licensee);
        }

        /// <summary>
        /// Gets the members missing created date.
        /// </summary>
        /// <value>Returns data of first deposit date.</value>
        public string FirstDepositDateData
        {
            get
            {
                LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod()));
                var commonService = new CommonDataService(Licensee, LogTracer);
                var webService = new WebDataService(Licensee, LogTracer);

                var usernames = _dataAccess.AffiliateMembersWhichMissCreatedDate;
                var systemName = commonService.SystemName;
                if (usernames.Any())
                {
                    var root = new MemberFirstDepositDate();
                    foreach (var username in usernames)
                    {
                        var custInfo = webService.GetLicenseeCustInfo(systemName, username);
                        LogTracer.Append(custInfo);
                        if (custInfo.IsError)
                        {
                            continue;
                        }

                        if (custInfo.FirstDepositDate != DateTime.MinValue)
                        {
                            root.Trans.Add(new MemberFirstDepositDateTrans
                            {
                                Username = username,
                                IBCusername = custInfo.IBCusername,
                                Date = custInfo.FirstDepositDate
                            });
                        }
                    }

                    if (root.Trans.Count > 0)
                    {
                        return XmlUtil.Serialize(root);
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the member info.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        /// <returns>Returns member information.</returns>
        public string GetMemberInfo(DateTime selectedDate)
        {
            LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), selectedDate));
            var commonService = new CommonDataService(Licensee, LogTracer);
            var webService = new WebDataService(Licensee, LogTracer);

            var systemName = commonService.SystemName;
            var xmlElement = webService.GetRegList(systemName, selectedDate);
            var xmlResult = XmlResult.Create(xmlElement);
            LogTracer.Append(xmlResult);
            if (xmlResult.Count > 0)
            {
                var transformer = new XmlTransformer("GetRegList.xslt");
                return transformer.Transform(xmlElement);
            }

            return null;
        }

        /// <summary>
        /// Imports the member information.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="date">The import date.</param>
        /// <returns>Returns import result.</returns>
        public Result ImportMemberInfo(string xmlData, DateTime date)
        {
            if (!string.IsNullOrEmpty(xmlData))
            {
                LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), xmlData, date));
                return _dataAccess.ImportAffiliateMemberInfo(xmlData, date);
            }

            return Result.Empty;
        }
   
        /// <summary>
        /// Updates the first deposit date.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>Return result.</returns>
        public Result UpdateFirstDepositDate(string xmlData)
        {
            if (!string.IsNullOrEmpty(xmlData))
            {
                LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), xmlData));
                return _dataAccess.UpdateAffiliateMemberCreatedDate(xmlData);
            }

            return Result.Empty;
        }
    }
}