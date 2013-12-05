// --------------------------------------------------------------------------
// <copyright file="FanexExtension.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataAccess.Util
{
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using Fanex.Data;

    /// <summary>
    /// FANEX Extension.
    /// </summary>
    public static class FanexExtension
    {
        /// <summary>
        /// Gets the output parameters.
        /// </summary>
        /// <param name="objectDb">The DB object.</param>
        /// <returns>Returns a hash table.</returns>
        public static Hashtable GetOutputParams(this ObjectDb objectDb)
        {
            var db = objectDb.Database;
            var command = objectDb.DbCommand;

            if (command.HasOutputParam())
            {
                var outputParam = new Hashtable();

                foreach (SqlParameter param in command.Parameters)
                {
                    if (param.IsNotReturnValue()
                        && param.Direction != ParameterDirection.Input)
                    {
                        object outputParamValue = db.GetParameterValue(command, param.ParameterName);
                        outputParam.Add(param.GetLowerName(), outputParamValue);
                    }
                }

                return outputParam;
            }

            return null;
        }
    }
}