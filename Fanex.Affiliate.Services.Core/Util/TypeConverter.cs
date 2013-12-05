// --------------------------------------------------------------------------
// <copyright file="TypeConverter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Core.Util
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Type Converter.
    /// </summary>
    public static class TypeConverter
    {
        /// <summary>
        /// Convert from string to a specified type.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="text">The object type name.</param>
        /// <returns>Returns T object.</returns>
        public static T FromString<T>(string text)
        {
            return (T)FromString(text, typeof(T));
        }

        /// <summary>
        /// Convert from string to a specified type.
        /// </summary>
        /// <param name="text">The object type name.</param>
        /// <param name="type">The object type.</param>
        /// <returns>Returns an object.</returns>
        public static object FromString(string text, Type type)
        {
            try
            {
                return Convert.ChangeType(text, type, CultureInfo.InvariantCulture);
            }
            catch
            {
                return type.IsValueType ? Activator.CreateInstance(type) : null;
            }
        }
    }
}