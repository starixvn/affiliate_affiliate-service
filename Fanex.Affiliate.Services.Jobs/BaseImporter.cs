namespace Fanex.Affiliate.Services.Jobs
{
    using System;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataService;
    using Fanex.Affiliate.Services.Jobs.DTO;
    using Fanex.Affiliate.Services.Jobs.Helpers;
    using Fanex.Affiliate.Services.Jobs.Importer;

    /// <summary>
    ///     A abstract import controller.
    /// </summary>
    public abstract class BaseImporter
    {
        /// <summary>
        ///     Used for log tracer.
        /// </summary>
        private static readonly string Delimiters = new string('*', 20);

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseImporter" /> class.
        /// </summary>
        protected BaseImporter()
        {
            LogTracer = LogTracer.Create();
            UmService = UmService.Create();
        }

        /// <summary>
        ///     Gets the log tracer.
        /// </summary>
        /// <value>
        ///     The log tracer.
        /// </value>
        protected LogTracer LogTracer { get; private set; }


        /// <summary>
        ///     Gets the um service.
        /// </summary>
        /// <value>
        ///     The um service.
        /// </value>
        protected UmService UmService { get; private set; }

        /// <summary>
        ///     Creates the specified import type.
        /// </summary>
        /// <param name="importType">Type of the import.</param>
        /// <param name="licensee">The licensee.</param>
        /// <returns>Returns an import controller.</returns>
        /// <exception cref="System.Exception">Initializing an import controller failed.</exception>
        public static BaseImporter Create(ImportType importType, Licensee licensee)
        {
            BaseImporter controller = null;
            switch (importType)
            {
                case ImportType.ImportExchangeRate:
                    controller = new ExchangeRateImporter(licensee);
                    break;

                case ImportType.ImportMemberDaily:
                    controller = new MemberDailyImporter(licensee);
                    break;

                case ImportType.ImportMemberInfo:
                    controller = new MemberInfoImporter(licensee);
                    break;
            }

            return controller;
        }

        /// <summary>
        ///     Imports (called from Service).
        /// </summary>
        /// <param name="importInfo">The import information.</param>
        public void Import(ImportInfo importInfo)
        {
            LogTracer.Clear();
            LogTracer.Append(string.Format("\n{0} - {1}", GetType().Name, importInfo.Licensee.Name));
            LogTracer.Append(Delimiters);
            Run(importInfo);
            LogTracer.Append(Delimiters);
            LogTracer.Write();
        }

        /// <summary>
        ///     Imports (called from WinForm).
        /// </summary>
        /// <param name="importInfo">The import information.</param>
        public void Import(ManuallyImportInfo importInfo)
        {
            LogTracer.Clear();
            LogTracer.Append(string.Format("\n{0} - {1}", GetType().Name, importInfo.Licensee.Name));
            LogTracer.Append(Delimiters);
            Run(importInfo);
            LogTracer.Append(Delimiters);
            LogTracer.Write();
        }

        /// <summary>
        ///     Processes the exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public void ProcessException(Exception ex)
        {
            LogTracer.Append(ex);

            LogHelper.Error(ex);
        }

        /// <summary>
        ///     Run import in service.
        /// </summary>
        /// <param name="importInfo">The import information.</param>
        protected abstract void Run(ImportInfo importInfo);

        /// <summary>
        ///     Run import in win form.
        /// </summary>
        /// <param name="importInfo">The import information.</param>
        protected abstract void Run(ManuallyImportInfo importInfo);

        /// <summary>
        ///     Gets the service.
        /// </summary>
        /// <typeparam name="T">A data service type.</typeparam>
        /// <param name="licensee">The licensee.</param>
        /// <returns>Return a data service.</returns>
        protected T CreateService<T>(Licensee licensee) where T : BaseDataService
        {
            var t = (T)Activator.CreateInstance(typeof(T), licensee);
            t.SetLogTracer(LogTracer);
            return t;
        }
    }
}