namespace Fanex.Affiliate.Services.Core
{
    using System;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// LogFormatter utility methods.
    /// </summary>
    public class LogFormatter
    {
        /// <summary>
        /// Formats method info.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="values">The array of values.</param>
        /// <returns>Returns string.</returns>
        public string Format(MethodBase methodInfo, params object[] values)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Method {0}:", methodInfo.Name);
            int i = 0;
            foreach (var parameter in methodInfo.GetParameters())
            {
                var value = values[i];
                if (value is DateTime)
                {
                    value = ((DateTime)value).ToShortDateString();
                }

                stringBuilder.AppendFormat("\n   {0}: {1}", parameter.Name, value);
                i++;
            }

            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Formats the specified object.
        /// </summary>
        /// <param name="value">The object.</param>
        /// <returns>Returns object in string.</returns>
        public string Format(object value)
        {
            var type = value.GetType();
            var properties = type.GetProperties();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Object {0}:", type.Name);
            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    var name = property.Name;
                    var propertyValue = property.GetValue(value, null);
                    stringBuilder.AppendFormat("\n   {0}: {1}", name, propertyValue);
                }
            }

            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }
    }
}