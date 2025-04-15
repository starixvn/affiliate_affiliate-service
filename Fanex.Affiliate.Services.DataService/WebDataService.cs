namespace Fanex.Affiliate.Services.DataService
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Xml.Linq;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataService.Api;
    using Fanex.Affiliate.Services.DataService.Xml;
    using Fanex.Affiliate.Services.DataService.Xml.Objects;
    using Refit;

    /// <summary>
    /// Xml Web Service Wrapper.
    /// </summary>
    public class WebDataService : BaseDataService
    {
        /// <summary>
        /// The web service.
        /// </summary>
        private static ISagaApi _sagaApi;

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

        private static readonly Lazy<ISagaApi> _instance = new Lazy<ISagaApi>(() =>
        {
            try
            {
                var apiUrl = ConfigurationManager.AppSettings["SagaApiUrl"];
                if (string.IsNullOrEmpty(apiUrl))
                {
                    throw new ConfigurationErrorsException("SagaApiUrl is not configured in AppSettings");
                }

                var isUsingProxy = Convert.ToBoolean(ConfigurationManager.AppSettings["IsUseProxyToGetSagaApi"]);
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = isUsingProxy ? GetConfiguredProxy() : null,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    UseCookies = false
                };

                var httpClient = new HttpClient(httpClientHandler)
                {
                    BaseAddress = new Uri(apiUrl)
                };

                return RestService.For<ISagaApi>(httpClient);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize SagaApi client", ex);
            }
        });

        private static ISagaApi SagaApi
        {
            get
            {
                return _sagaApi ?? (_sagaApi = _instance.Value);
            }
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

            var param = new RegParam
            {
                StrSiteName = siteName,
                DtimeTargetDate = targetDate
            };

            var rawXml = SagaApi.GetRegList(param).Result;

            return GetXElement(rawXml);
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
            var element = HandleGetLicenseeCustInfo(siteName, licenseeUserName);
            return XmlConverter.To<GetLicenseeCustInfo>(element);
        }

        private XElement HandleGetLicenseeCustInfo(string siteName, string licenseeUserName)
        {
            var param = new LicenseCustInfoParam
            {
                StrSiteName = siteName,
                StrLicenseeUserName = licenseeUserName
            };

            var rawXml = SagaApi.GetLicenseeCustInfo(param).Result;

            return GetXElement(rawXml);
        }

        private static IWebProxy GetConfiguredProxy()
        {
            var proxyUrl = ConfigurationManager.AppSettings["ApiProxyUrl"];
            if (string.IsNullOrEmpty(proxyUrl))
            {
                throw new ConfigurationErrorsException("ApiProxyUrl is not configured in AppSettings when proxy is enabled");
            }

            return new WebProxy(new Uri(proxyUrl));
        }

        private static XElement GetXElement(string xmlString)
        {
            try
            {
                return XDocument.Parse(xmlString).Root;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}