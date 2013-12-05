// --------------------------------------------------------------------------
// <copyright file="CommonDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataAccess.Implement
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
        /// Get System Setting by name.
        /// </summary>
        /// <param name="name">The system name.</param>
        /// <returns>Returns DTO object.</returns>
        public Aff_SystemSettingsSelByName Aff_SystemSettingsSelByName(string name)
        {
            try
            {
                var param = new Hashtable { { "name", name } };

                using (var reader = ExecSpDataReader("Aff_SystemSettingsSelByName", param))
                {
                    while (reader.Read())
                    {
                        return new Aff_SystemSettingsSelByName
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
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return null;
        }

        /// <summary>
        /// Get System Setting by ID.
        /// </summary>
        /// <param name="id">The system ID.</param>
        /// <returns>Returns DTO object.</returns>
        public Aff_SystemSettingsSelByID Aff_SystemSettingsSelByID(int id)
        {
            try
            {
                var param = new Hashtable { { "id", id } };

                using (var reader = ExecSpDataReader("Aff_SystemSettingsSelByID", param))
                {
                    while (reader.Read())
                    {
                        return new Aff_SystemSettingsSelByID
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
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return null;
        }

        /// <summary>
        /// Get Current Date.
        /// </summary>
        /// <returns>Returns the current date.</returns>
        public DateTime Aff_CurrentDate()
        {
            try
            {
                using (var reader = ExecSpDataReader("Aff_CurrentDate"))
                {
                    while (reader.Read())
                    {
                        return DbConverter.ToDateTime(reader.GetValue(1));
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

            return DateTime.Today;
        }
    }
}