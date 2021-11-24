namespace Fanex.Affiliate.Services.WinForm
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Windows.Forms;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Data.Configuration;
    using Fanex.Logging;
    using Fanex.Logging.Sentry.RavenClient;
    using SharpRaven;

    /// <summary>
    /// Entry Point Class.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetupSentryLogging();
            DbSettingProviderManager.Current.Start();
            AppSetting.Initialize();
            LicenseeManager.Initialize();
            Application.Run(new MainForm());
        }

        private static void SetupSentryLogging()
        {
            var appSetting = ConfigurationManager.AppSettings;

            var env = appSetting.Get("Environment");
            var backEndDsn = appSetting.Get("SentryBackEndDsn");
            var category = appSetting.Get("LoggingCategory");

            if(!string.IsNullOrEmpty(backEndDsn))
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