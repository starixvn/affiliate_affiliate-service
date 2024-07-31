using Fanex.Caching;
using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using ONEbook.UM;
using ONEbook.UM.Configurations;
using ONEbook.UM.Helpers;
using ONEbook.UM.NETFramework;
using ONEbook.UM.Transport;
using SharpRaven;
using System.Collections.Generic;
using System.Configuration;

namespace Fanex.Affiliate.Services.Core.Configuration
{
    public static class FanexService
    {
        public static void SetUpUMEnviroment()
        {
            var umConfig = new UMClientConfiguration().Initialize();

            UMClientManager
               .UseConfig(umConfig)
               .UseCacheService(new UMCacheService(new CacheService()))
               .UseRestClient(new UMRestClient(umConfig))
               .UseHttpContext(new EmptyHttpContextHelper());
        }

        public static void SetupSentryLogging()
        {
            var appSetting = ConfigurationManager.AppSettings;

            var env = appSetting.Get("Environment");
            var backEndDsn = appSetting.Get("SentryBackEndDsn");
            var category = appSetting.Get("LoggingCategory");

            if (!string.IsNullOrEmpty(backEndDsn))
            {
                LogManager.SetDefaultLogCategory(category).Use(new List<ILogEngine>
                {
                    new SentryLogging(new SentryEngineOptions
                    {
                        Dsn = new Dsn(backEndDsn),
                        Environment = env
                    })
                });
            }
        }
    }
}
