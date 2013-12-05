// --------------------------------------------------------------------------
// <copyright file="MemberFirstDepositDateTrans.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="YYYY.MM.DD">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataService.Xml.Objects
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The XML object.
    /// </summary>
    public class MemberFirstDepositDateTrans
    {
        /// <summary>
        /// Gets or sets Username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [XmlAttribute("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets IBC username.
        /// </summary>
        /// <value>
        /// The IBC username.
        /// </value>
        [XmlAttribute("ibcusername")]
        public string IBCusername { get; set; }

        /// <summary>
        /// Gets or sets Date.
        /// </summary>
        /// <value>
        /// The object date.
        /// </value>
        [XmlAttribute("date")]
        public DateTime Date { get; set; }
    }
}