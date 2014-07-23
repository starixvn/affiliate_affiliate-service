// --------------------------------------------------------------------------
// <copyright file="FileUtil.cs" company="Nexcel Solutions Vietnam">
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
    using System.Security;

    /// <summary>
    /// File Utility Methods.
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// Resolve a file path from a relative path to an absolute path.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Returns the absolute path.</returns>
        public static string ResolveFile(string fileName)
        {
            if (fileName != null && fileName.StartsWith("~", StringComparison.CurrentCultureIgnoreCase))
            {
                fileName = fileName.Substring(1);
                if (fileName.StartsWith("/", StringComparison.CurrentCultureIgnoreCase) || fileName.StartsWith("\\", StringComparison.CurrentCultureIgnoreCase))
                {
                    fileName = fileName.Substring(1);
                }

                try
                {
                    fileName = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, fileName);
                }
                catch (SecurityException)
                {
                    Logger.Error(string.Format("Cannot determine path for relative file '{0}' because of security exception", fileName));
                    throw;
                }
            }

            return fileName;
        }
    }
}