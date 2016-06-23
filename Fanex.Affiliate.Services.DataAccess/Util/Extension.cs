namespace Fanex.Affiliate.Services.DataAccess.Util
{
    using Fanex.Data.DNAide;

    /// <summary>
    /// Common extension.
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Convert DB result to DTO result.
        /// </summary>
        /// <param name="result">The DB result.</param>
        /// <returns>Returns a result.</returns>
        public static Result ToResult(this DbResult result)
        {
            if (result.IsValid())
            {
                return new Result(DbConverter.ToInt32(result.Outputs["errcode"]), result.Outputs["errmsg"].ToString());
            }

            return Result.Empty;
        }

        /// <summary>
        /// Determines whether DB result object is valid.
        /// </summary>
        /// <param name="r">The DB result.</param>
        /// <returns>Returns boolean.</returns>
        public static bool IsValid(this DbResult r)
        {
            if (r != null && r.Outputs != null && r.Outputs.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}