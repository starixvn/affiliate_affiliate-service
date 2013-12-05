// --------------------------------------------------------------------------
// <copyright file="XsltFunctions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.DataService.Xml
{
    /// <summary>
    /// XSLT Functions to apply into XSLT template.
    /// </summary>
    public class XsltFunctions
    {
        /// <summary>
        /// Gets Affiliate Id from BTAG.
        /// </summary>
        /// <param name="btag">The BTAG value.</param>
        /// <returns>Return affiliate ID.</returns>
        public string GetAffId(string btag)
        {
            var strings = btag.Split('_');
            if (strings.Length > 1)
            {
                return strings[1];
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets ClickId from BTAG.
        /// </summary>
        /// <param name="btag">The BTAG value.</param>
        /// <returns>Return Click Id.</returns>
        public string GetClickId(string btag)
        {
            var strings = btag.Split('_');
            if (strings.Length > 2)
            {
                return strings[2];
            }

            return string.Empty;
        }
    }
}