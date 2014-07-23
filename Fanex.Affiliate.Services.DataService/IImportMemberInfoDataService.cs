namespace Fanex.Affiliate.Services.DataService
{
    using System;

    public interface IImportMemberInfoDataService
    {
        /// <summary>
        /// Gets the member info.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        /// <returns>Return member information.</returns>
        string GetMemberInfo(DateTime selectedDate);
    }
}