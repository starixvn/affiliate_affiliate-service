namespace Fanex.Affiliate.Services.DataAccess.Util
{
    using System;

    /// <summary>
    /// Convert value from store procedure result.
    /// </summary>
    public static class DbConverter
    {
        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns boolean.</returns>
        public static bool ToBoolean(object value)
        {
            if (IsValid(value))
            {
                bool result;
                return bool.TryParse(value.ToString(), out result) ? result : false;
            }

            return false;
        }

        /// <summary>
        /// To the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns byte.</returns>
        public static byte ToByte(object value)
        {
            byte result = 0;
            if (IsValid(value))
            {
                return byte.TryParse(value.ToString(), out result) ? result : (byte)0;
            }

            return result;
        }

        /// <summary>
        /// To the short.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns short.</returns>
        public static short ToInt16(object value)
        {
            short result = 0;
            if (IsValid(value))
            {
                return short.TryParse(value.ToString(), out result) ? result : (short)0;
            }

            return result;
        }

        /// <summary>
        /// To the integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns integer.</returns>
        public static int ToInt32(object value)
        {
            int result = 0;
            if (IsValid(value))
            {
                return int.TryParse(value.ToString(), out result) ? result : 0;
            }

            return result;
        }

        /// <summary>
        /// To the long.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns long.</returns>
        public static long ToInt64(object value)
        {
            long result = 0;
            if (IsValid(value))
            {
                return long.TryParse(value.ToString(), out result) ? result : (long)0;
            }

            return result;
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns double.</returns>
        public static double ToDouble(object value)
        {
            double result = 0.0d;
            if (IsValid(value))
            {
                return double.TryParse(value.ToString(), out result) ? result : (double)0;
            }

            return result;
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns decimal.</returns>
        public static decimal ToDecimal(object value)
        {
            decimal result = 0.0m;
            if (IsValid(value))
            {
                return decimal.TryParse(value.ToString(), out result) ? result : (decimal)0;
            }

            return result;
        }

        /// <summary>
        /// To the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(object value)
        {
            string result = string.Empty;
            if (IsValid(value))
            {
                result = Convert.ToString(value);
            }

            return result;
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Returns date time.</returns>
        public static DateTime ToDateTime(object value)
        {
            DateTime result = DateTime.MinValue;
            if (IsValid(value))
            {
                return DateTime.TryParse(value.ToString(), out result) ? result : DateTime.MinValue;
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsValid(object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrEmpty(value.ToString()))
            {
                return false;
            }

            return true;
        }
    }
}