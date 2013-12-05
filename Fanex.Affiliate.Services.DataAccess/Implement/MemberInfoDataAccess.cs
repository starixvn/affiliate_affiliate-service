// --------------------------------------------------------------------------
// <copyright file="MemberInfoDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataAccess.DTO;
    using Fanex.Affiliate.Services.DataAccess.Util;

    /// <summary>
    /// Import Member Information DataAccess.
    /// </summary>
    public class MemberInfoDataAccess : BaseDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInfoDataAccess"/> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        public MemberInfoDataAccess(Licensee licensee)
            : base(licensee)
        {
        }

        /// <summary>
        /// Import member information.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <param name="registeredDate">The registered date.</param>
        /// <returns>Returns result.</returns>
        public Result Task_Aff_MemberInfoImportData(string xmlData, DateTime registeredDate)
        {
            try
            {
                var param = new Hashtable
                {
                    { "adminid", AppSetting.AdminId },
                    { "xmldata", xmlData },
                    { "registereddate", registeredDate },
                    { "errcode", 0 },
                    { "errmsg", string.Empty }
                };

                var result = ExecSp("Task_Aff_MemberInfoImportData", param);
                return result.ToResult();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return Result.Empty;
        }

        /// <summary>
        /// Get Missing Created Dates.
        /// </summary>
        /// <returns>Returns a list of DTO object.</returns>
        public List<Aff_MemberInfoGetMissingCreatedDate> Aff_MemberInfoGetMissingCreatedDate()
        {
            var result = new List<Aff_MemberInfoGetMissingCreatedDate>();

            try
            {
                using (var reader = ExecSpDataReader("Aff_MemberInfoGetMissingCreatedDate"))
                {
                    while (reader.Read())
                    {
                        result.Add(new Aff_MemberInfoGetMissingCreatedDate
                        {
                            Username = reader.ToString("username")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return result;
        }

        /// <summary>
        /// Update member info.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>Returns result.</returns>
        public Result Task_Aff_MemberInfoUpdateCreatedDate(string xmlData)
        {
            try
            {
                var param = new Hashtable { { "xmldata", xmlData }, { "errcode", 0 }, { "errmsg", string.Empty } };

                var result = ExecSp("Task_Aff_MemberInfoUpdateCreatedDate", param);
                return result.ToResult();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return Result.Empty;
        }
    }
}