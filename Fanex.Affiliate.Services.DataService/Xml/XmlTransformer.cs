// --------------------------------------------------------------------------
// <copyright file="XmlTransformer.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataService.Xml
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Xsl;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.Core.Util;

    /// <summary>
    /// Xml Transformer.
    /// </summary>
    public class XmlTransformer
    {
        /// <summary>
        /// The XSLT embedded resource.
        /// </summary>
        private readonly string _xsltEmbbedResource;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTransformer"/> class.
        /// </summary>
        /// <param name="xsltEmbbedResource">The XSLT embedded resource.</param>
        public XmlTransformer(string xsltEmbbedResource)
        {
            _xsltEmbbedResource = xsltEmbbedResource;
        }

        /// <summary>
        /// Transforms XElement to string.
        /// </summary>
        /// <param name="element">The XML element.</param>
        /// <returns>Return XML string.</returns>
        public string Transform(XElement element)
        {
            var transform = new XslCompiledTransform();
            var xsltArgs = new XsltArgumentList();
            xsltArgs.AddExtensionObject("urn:XsltFunctions", new XsltFunctions());
            var resourcePath = Constants.XsltTemplatePath + _xsltEmbbedResource;
            string xsltContent = ResourceUtil.ReadFromEmbeddedAssemblyResource(resourcePath);
            using (var stringReader = new StringReader(xsltContent))
            {
                using (var xmlReader = XmlReader.Create(stringReader))
                {
                    transform.Load(xmlReader);
                    using (var stringWriter = new StringWriter())
                    {
                        var settings = new XmlWriterSettings
                        {
                            OmitXmlDeclaration = true,
                        };

                        using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                        {
                            transform.Transform(element.CreateReader(), xsltArgs, xmlWriter);
                            return stringWriter.ToString();
                        }
                    }
                }
            }
        }
    }
}