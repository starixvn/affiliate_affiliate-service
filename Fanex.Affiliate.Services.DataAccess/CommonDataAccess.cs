// --------------------------------------------------------------------------
// <copyright file="CommonDataAccess.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.DataAccess.DTO;
    using Fanex.Affiliate.Services.DataAccess.Util;

    /// <summary>
    /// Common DataAccess.
    /// </summary>
    public class CommonDataAccess : BaseDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonDataAccess"/> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        public CommonDataAccess(Licensee licensee)
            : base(licensee)
        {
        }

        /// <summary>
        /// Gets Current Date.
        /// </summary>
        /// <value>Returns the current date.</value>
        public DateTime AffiliateCurrentDate
        {
            get
            {
                using (var reader = ExecSpDataReader("Aff_CurrentDate"))
                {
                    return reader.Read()
                        ? DbConverter.ToDateTime(reader.GetValue(1))
                        : DateTime.Today;
                }
            }
        }

        /// <summary>
        /// Get System Setting by name.
        /// </summary>
        /// <param name="name">The system name.</param>
        /// <returns>Returns DTO object.</returns>
        public SystemSettings GetAffiliateSystemSettingsByName(string name)
        {
            var param = new Hashtable { { "name", name } };

            using (var reader = ExecSpDataReader("Aff_SystemSettingsSelByName", param))
            {
                if (reader.Read())
                {
                    return new SystemSettings
                    {
                        SID = reader.ToInt32("sid"),
                        Type = reader.ToString("type"),
                        SName = reader.ToString("sname"),
                        SValue = reader.ToString("svalue"),
                        Description = reader.ToString("description"),
                        Environment = reader.ToString("environment"),
                    };
                }
            }

            return null;
        }

        /// <summary>
        /// Get System Setting by ID.
        /// </summary>
        /// <param name="id">The system ID.</param>
        /// <returns>Returns DTO object.</returns>
        public SystemSettings GetAffiliateSystemSettingsById(int id)
        {
            var param = new Hashtable { { "id", id } };

            using (var reader = ExecSpDataReader("Aff_SystemSettingsSelByID", param))
            {
                if (reader.Read())
                {
                    return new SystemSettings
                    {
                        SID = reader.ToInt32("sid"),
                        Type = reader.ToString("meta"),
                        SName = reader.ToString("sname"),
                        SValue = reader.ToString("svalue"),
                        Description = reader.ToString("description"),
                        Environment = reader.ToString("environment"),
                    };
                }
            }

            return null;
        }
    }
}