namespace Fanex.Affiliate.Services.DataService.Xml
{
    using System;
    using System.Data;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Fanex.Affiliate.Services.Core.Util;
    using Fanex.Affiliate.Services.DataService.Xml.Objects;

    /// <summary>
    /// Xml Converter.
    /// </summary>
    public static class XmlConverter
    {
        /// <summary>
        /// Convert a data table to xml string.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <param name="rowFrom">From row value.</param>
        /// <param name="rowTo">To row value.</param>
        /// <returns>Return XML string.</returns>
        public static string ToXmlString(DataTable dataTable, int rowFrom = 0, int rowTo = 0)
        {
            var xmlDocument = new XmlDocument();
            XmlElement root = xmlDocument.CreateElement("root");
            xmlDocument.AppendChild(root);
            if (dataTable == null)
            {
                return xmlDocument.OuterXml;
            }

            if (rowTo == 0)
            {
                rowTo = dataTable.Rows.Count;
            }

            for (var i = rowFrom; i < rowTo; i++)
            {
                XmlElement transNode = xmlDocument.CreateElement("trans");
                for (var j = 0; j < dataTable.Columns.Count; j++)
                {
                    transNode.SetAttribute(dataTable.Columns[j].ColumnName.ToLower(), dataTable.Rows[i][j].ToString());
                }

                root.AppendChild(transNode);
            }

            return xmlDocument.OuterXml;
        }

        /// <summary>
        /// Convert XElement to an object.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="container">The XML element.</param>
        /// <returns>Return object T.</returns>
        public static T To<T>(XContainer container) where T : XmlObject
        {
            var type = typeof(T);
            var result = (T)Activator.CreateInstance(type);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var name = property.Name;
                if (container.Element(name) != null)
                {
                    var value = TypeConverter.FromString(container.Element(name).Value, property.PropertyType);
                    property.SetValue(result, value, null);
                }
            }

            return result;
        }
    }
}