namespace Fanex.Affiliate.Services.DataService.Xml.Objects
{
    using Fanex.Affiliate.Services.Core;

    /// <summary>
    /// Xml object.
    /// </summary>
    public class XmlObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlObject"/> class.
        /// </summary>
        public XmlObject()
        {
            ReturnCode = -1;
        }

        /// <summary>
        /// Gets or sets a return code.
        /// </summary>
        /// <value>
        /// The return code.
        /// </value>
        public int ReturnCode { get; set; }

        /// <summary>
        /// Gets or sets a return message.
        /// </summary>
        /// <value>
        /// The return message.
        /// </value>
        public string ReturnMessage { get; set; }

        /// <summary>
        /// Gets a value indicating whether the result is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if return code is not zero; otherwise, <c>false</c>.
        /// </value>
        public bool IsError
        {
            get
            {
                if (ReturnCode != 0)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var logFormatter = new LogFormatter();
            return logFormatter.Format(this);
        }
    }
}