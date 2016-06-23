namespace Fanex.Affiliate.Services.Core.Util
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Resource Utility Methods.
    /// </summary>
    public static class ResourceUtil
    {
        /// <summary>
        /// Get string from embedded assembly resource.
        /// </summary>
        /// <param name="type">The object type.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>Returns embedded resource content.</returns>
        public static string ReadFromEmbeddedAssemblyResource(Type type, string resourceName)
        {
            var resourcePath = type.Namespace + "." + resourceName;
            return ReadFromEmbeddedAssemblyResource(resourcePath);
        }

        /// <summary>
        /// Get string from embedded assembly resource.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns>Returns embedded resource content.</returns>
        public static string ReadFromEmbeddedAssemblyResource(string resourcePath)
        {
            var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourcePath);
            if (stream == null)
            {
                return string.Empty;
            }

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}