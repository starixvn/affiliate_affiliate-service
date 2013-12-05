// --------------------------------------------------------------------------
// <copyright file="LogTracer.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Leo.Vo" date="2013.06.25">Created</change>
// </history>

namespace Fanex.Affiliate.Services.Core
{
    using System;
    using System.Text;
    using Fanex.Affiliate.Services.Core.Util;

    /// <summary>
    /// Support log tracking.
    /// </summary>
    public class LogTracer
    {
        /// <summary>
        /// StringBuilder object.
        /// </summary>
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        /// <summary>
        /// Determinate whether log has error.
        /// </summary>
        private bool _isError;

        /// <summary>
        /// Prevents a default instance of the <see cref="LogTracer"/> class from being created.
        /// </summary>
        private LogTracer()
        {
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>Returns LogTracer object.</returns>
        public static LogTracer Create()
        {
            return new LogTracer();
        }

        /// <summary>
        /// Appends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Append(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                _stringBuilder.AppendLine(message);
            }
        }

        /// <summary>
        /// Appends the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Append(object obj)
        {
            if (obj != null)
            {
                _stringBuilder.AppendLine(obj.ToString());
            }
        }

        /// <summary>
        /// Appends the specified exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public void Append(Exception ex)
        {
            _isError = true;
            string message = ExceptionFormatter.GetMessage(ex);
            _stringBuilder.AppendLine(message);
        }

        /// <summary>
        /// Writes log messages.
        /// </summary>
        public void Write()
        {
            var msg = _stringBuilder.ToString();
            if (!string.IsNullOrEmpty(msg))
            {
                if (_isError)
                {
                    Logger.Error(msg);
                }
                else
                {
                    Logger.Info(msg);
                }

                Clear();
            }
        }

        /// <summary>
        /// Clears log messages.
        /// </summary>
        public void Clear()
        {
            _isError = false;
            _stringBuilder.Clear();
        }
    }
}