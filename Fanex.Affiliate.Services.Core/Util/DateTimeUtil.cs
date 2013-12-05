// --------------------------------------------------------------------------
// <copyright file="DateTimeUtil.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Core.Util
{
    using System;
    using NodaTime;

    /// <summary>
    /// DateTime utility methods.
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// Convert a string in format "HH:MM:SS" to type of DateTime with current date.
        /// </summary>
        /// <param name="s">Date time in string.</param>
        /// <returns>
        /// Returns a date time.
        /// </returns>
        public static LocalTime GetLocalTime(string s)
        {
            var result = LocalTime.Midnight;
            var strings = s.Split(':');
            if (strings.Length == 3)
            {
                int hour = Convert.ToInt32(strings[0]);
                int minute = Convert.ToInt32(strings[1]);
                int second = Convert.ToInt32(strings[2]);
                result = new LocalTime(hour, minute, second);
            }

            return result;
        }
    }
}