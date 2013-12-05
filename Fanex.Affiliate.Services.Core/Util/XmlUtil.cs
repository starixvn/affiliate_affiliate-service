// --------------------------------------------------------------------------
// <copyright file="XmlUtil.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Core.Util
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// XML Utility Methods.
    /// </summary>
    public static class XmlUtil
    {
        /// <summary>
        /// Validates the XML by XSD.
        /// </summary>
        /// <param name="xml">The XML string.</param>
        /// <param name="xsdLocation">The XSD location.</param>
        /// <exception cref="System.Exception">Unable to validate XML with schema:  + ex.Message.</exception>
        public static void ValidateXml(string xml, string xsdLocation)
        {
            try
            {
                var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema };
                settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(xsdLocation);
                var schema = XmlSchema.Read(stream, XmlValidationCallBack);
                settings.Schemas.Add(schema);
                settings.ValidationEventHandler += XmlValidationCallBack;

                // stream to validate
                using (var reader = XmlReader.Create(new StringReader(xml), settings))
                {
                    while (reader.Read())
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to validate XML with schema: " + ex.Message);
            }
        }

        /// <summary>
        /// Serializes the specified object to Xml string.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>Returns a XML string.</returns>
        public static string Serialize<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());
            var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, obj);
            return textWriter.ToString();
        }

        /// <summary>
        /// XML Validation Callback.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ValidationEventArgs"/> instance containing the event data.</param>
        private static void XmlValidationCallBack(object sender, ValidationEventArgs e)
        {
            Logger.Error(e.Message);
        }
    }
}