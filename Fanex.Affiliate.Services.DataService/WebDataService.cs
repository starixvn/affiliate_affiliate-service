namespace Fanex.Affiliate.Services.DataService
{
    using System;
    using System.Reflection;
    using System.Xml.Linq;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataService.AffiliateWebService;
    using Fanex.Affiliate.Services.DataService.Xml;
    using Fanex.Affiliate.Services.DataService.Xml.Objects;

    /// <summary>
    /// Xml Web Service Wrapper.
    /// </summary>
    public class WebDataService : BaseDataService
    {
        /// <summary>
        /// The web service.
        /// </summary>
        private static readonly ServiceSoapClient AffiliateWebService = new ServiceSoapClient("ServiceSoap");

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDataService"/> class.
        /// </summary>
        /// <param name="licensee">The licensee information.</param>
        public WebDataService(Licensee licensee)
            : base(licensee)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDataService"/> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        /// <param name="logTracer">The log tracer.</param>
        public WebDataService(Licensee licensee, LogTracer logTracer)
            : this(licensee)
        {
            LogTracer = logTracer;
        }

        /// <summary>
        /// Gets the register list.
        /// </summary>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="targetDate">The target date.</param>
        /// <returns>Returns XML element.</returns>
        public XElement GetRegList(string siteName, DateTime targetDate)
        {
            LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), siteName, targetDate));
            return AffiliateWebService.GetRegList(siteName, targetDate);
        }

        /// <summary>
        /// Gets the licensee customer info.
        /// </summary>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="licenseeUserName">Name of the licensee user.</param>
        /// <returns>Returns license customer information.</returns>
        public GetLicenseeCustInfo GetLicenseeCustInfo(string siteName, string licenseeUserName)
        {
            LogTracer.Append(LogFormatter.Format(MethodBase.GetCurrentMethod(), siteName, licenseeUserName));
            var element = AffiliateWebService.GetLicenseeCustInfo(siteName, licenseeUserName);
            return XmlConverter.To<GetLicenseeCustInfo>(element);
        }
    }
}