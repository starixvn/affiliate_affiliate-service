namespace Fanex.Affiliate.Services.DataService.Xml.Objects
{
    using System.Xml.Linq;

    /// <summary>
    /// Xml result of executing xml web service.
    /// </summary>
    public class XmlResult : XmlObject
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The total of objects from xml service result.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Creates the XmlResult from XElement.
        /// </summary>
        /// <param name="container">The xml element.</param>
        /// <returns>Return XML result.</returns>
        public static XmlResult Create(XContainer container)
        {
            return XmlConverter.To<XmlResult>(container);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ReturnCode != 0 
                ? string.Format("ReturnCode: {0}, ReturnMessage: {1}", ReturnCode, ReturnMessage) 
                : string.Format("Count: {0}", Count);
        }
    }
}