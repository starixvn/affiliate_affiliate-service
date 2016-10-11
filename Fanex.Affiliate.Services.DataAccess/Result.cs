namespace Fanex.Affiliate.Services.DataAccess
{
    /// <summary>
    /// Result of executing stored procedure.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="code">Result code.</param>
        /// <param name="message">Result message.</param>
        public Result(int code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        public Result()
            : this(-1, string.Empty)
        {
        }

        /// <summary>
        /// Gets an empty value.
        /// </summary>
        /// <value>
        /// The empty value.
        /// </value>
        public static Result Empty
        {
            get
            {
                return new Result();
            }
        }

        /// <summary>
        /// Gets or sets Result code.
        /// </summary>
        /// <value>
        /// The code value.
        /// </value>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets Result message.
        /// </summary>
        /// <value>
        /// The message string.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets a value indicating whether result status is successful or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if code is zero otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessful
        {
            get
            {
                return Code == 0;
            }
        }
    }
}