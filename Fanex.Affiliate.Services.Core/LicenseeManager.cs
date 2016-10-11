namespace Fanex.Affiliate.Services.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Affiliate.Services.Core.Util;

    /// <summary>
    /// Manage list of licensees.
    /// </summary>
    public static class LicenseeManager
    {
        /// <summary>
        /// The configuration data object.
        /// </summary>
        private static LicenseeXmlConfiguration _data;

        /// <summary>
        /// Gets the licensees.
        /// </summary>
        /// <value>
        /// The list of licensees.
        /// </value>
        public static List<Licensee> Licensees
        {
            get
            {
                return _data.Licensees;
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            Initialize("~/Licensees.xml");
        }

        /// <summary>
        /// Initializes this instance from configuration file.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <exception cref="System.Exception">
        /// Initialize only one time
        /// or
        /// Data after initialization was null.
        /// </exception>
        public static void Initialize(string fileName)
        {
            string filePath = FileUtil.ResolveFile(fileName);
            using (var reader = new StreamReader(filePath))
            {
                string xml = reader.ReadToEnd();

                // validate XML
                XmlUtil.ValidateXml(xml, "Fanex.Affiliate.Services.Core.Configuration.LicenseeXmlConfiguration.xsd");

                // deserialize XML to object
                var xs = new XmlSerializer(typeof(LicenseeXmlConfiguration));
                _data = (LicenseeXmlConfiguration)xs.Deserialize(new StringReader(xml));               
            }
        }

        /// <summary>
        /// Gets the licensee by name.
        /// </summary>
        /// <param name="name">The licensee name.</param>
        /// <returns>Returns licensee object.</returns>
        public static Licensee GetLicensee(string name)
        {
            return _data.Licensees.FirstOrDefault(licensee => name.Equals(licensee.Name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}