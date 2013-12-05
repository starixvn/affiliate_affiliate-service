// --------------------------------------------------------------------------
// <copyright file="DataReaderExtension.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataAccess.Util
{
    using System;
    using System.Data;

    /// <summary>
    /// DataReader Extensions.
    /// </summary>
    public static class DataReaderExtension
    {
        /// <summary>
        /// Convert reader's field value to the boolean.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Returns boolean.</returns>
        public static bool ToBoolean(this IDataReader reader, string fieldName)
        {
            return DbConverter.ToBoolean(reader[fieldName]);
        }

        /// <summary>
        /// Convert reader's field value to the byte.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Returns byte.</returns>
        public static byte ToByte(this IDataReader reader, string fieldName)
        {
            return DbConverter.ToByte(reader[fieldName]);
        }

        /// <summary>
        /// Convert reader's field value to the short.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Returns short.</returns>
        public static short ToInt16(this IDataReader reader, string fieldName)
        {
            return DbConverter.ToInt16(reader[fieldName]);
        }

        /// <summary>
        /// Convert reader's field value to the integer.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Returns integer.</returns>
        public static int ToInt32(this IDataReader reader, string fieldName)
        {
            return DbConverter.ToInt32(reader[fieldName]);
        }

        /// <summary>
        /// Convert reader's field value to the long.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Returns long.</returns>
        public static long ToInt64(this IDataReader reader, string fieldName)
        {
            return DbConverter.ToInt64(reader[fieldName]);
        }

        /// <summary>
        /// Convert reader's field value to the double.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Returns double.</returns>
        public static double ToDouble(this IDataReader reader, string fieldName)
        {
            return DbConverter.ToDouble(reader[fieldName]);
        }

        /// <summary>
        /// Convert reader's field value to the decimal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Returns decimal.</returns>
        public static decimal ToDecimal(this IDataReader reader, string fieldName)
        {
            return DbConverter.ToDecimal(reader[fieldName]);
        }

        /// <summary>
        /// Convert reader's field value to string.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this IDataReader reader, string fieldName)
        {
            return DbConverter.ToString(reader[fieldName]);
        }

        /// <summary>
        /// Convert reader's field value to the date time.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Returns date time.</returns>
        public static DateTime ToDateTime(this IDataReader reader, string fieldName)
        {
            return DbConverter.ToDateTime(reader[fieldName]);
        }
    }
}