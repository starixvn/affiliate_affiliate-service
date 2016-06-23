namespace Fanex.Affiliate.Services.Core.Util
{
    using System;

    /// <summary>
    /// Format an exception as a string.
    /// </summary>
    public static class ExceptionFormatter
    {
        /// <summary>
        /// Gets the message from an exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>Returns a message.</returns>
        public static string GetMessage(Exception ex)
        {
            string message = string.Empty;
            if (ex != null)
            {
                message += ex.Message;
                int i = 1;
                while (ex.InnerException != null)
                {
                    message += string.Format("\n- Inner {0}: {1}", i, ex.InnerException.Message);
                    i++;
                    ex = ex.InnerException;
                }
            }

            return message;
        }
    }
}