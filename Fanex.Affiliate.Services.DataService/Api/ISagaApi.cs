using Refit;
using System;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Services.DataService.Api
{
    public interface ISagaApi
    {
        [Post("/GetLicenseeCustInfo")]
        Task<string> GetLicenseeCustInfo(LicenseCustInfoParam param);

        [Post("/GetRegList")]
        Task<string> GetRegList(RegParam param);
    }
    public class LicenseCustInfoParam
    {
        public string StrSiteName { get; set; }

        public string StrLicenseeUserName { get; set; }
    }

    public class RegParam
    {
        public string StrSiteName { get; set; }

        public DateTime DtimeTargetDate { get; set; }
    }
}
