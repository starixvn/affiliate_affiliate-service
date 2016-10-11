namespace Fanex.Affiliate.Services.DataService.Xml.Objects
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// The XML object.
    /// </summary>
    [XmlRootAttribute("root")]
    public class MemberFirstDepositDate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberFirstDepositDate"/> class.
        /// </summary>
        public MemberFirstDepositDate()
        {
            Trans = new List<MemberFirstDepositDateTrans>();
        }

        /// <summary>
        /// Gets or sets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        [XmlElementAttribute("trans")]
        public List<MemberFirstDepositDateTrans> Trans { get; set; }
    }
}