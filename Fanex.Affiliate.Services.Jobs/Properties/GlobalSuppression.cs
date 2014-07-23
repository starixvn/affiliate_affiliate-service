using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage(
    "Microsoft.Design", 
    "CA1031:DoNotCatchGeneralExceptionTypes",
    Scope = "member", Target = "Fanex.Affiliate.Services.Jobs.Importer.MemberInfoImporter.#Run(Fanex.Affiliate.Services.Jobs.DTO.ImportInfo)",
    Justification = "Handle general exception so that the service can continue regardless of exception")]
[module: SuppressMessage(
    "Microsoft.Design", 
    "CA1031:DoNotCatchGeneralExceptionTypes",
    Scope = "member", Target = "Fanex.Affiliate.Services.Jobs.Importer.MemberDailyImporter.#Run(Fanex.Affiliate.Services.Jobs.DTO.ImportInfo)",
    Justification = "Handle general exception so that the service can continue regardless of exception")]
[module: SuppressMessage(
    "Microsoft.Design", 
    "CA1031:DoNotCatchGeneralExceptionTypes",
    Scope = "member", Target = "Fanex.Affiliate.Services.Jobs.Importer.ExchangeRateImporter.#Run(Fanex.Affiliate.Services.Jobs.DTO.ImportInfo)",
    Justification = "Handle general exception so that the service can continue regardless of exception")]