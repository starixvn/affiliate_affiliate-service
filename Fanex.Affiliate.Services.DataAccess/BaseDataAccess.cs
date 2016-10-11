namespace Fanex.Affiliate.Services.DataAccess
{
    using System;
    using System.Collections;
    using System.Data;
    using Fanex.Affiliate.Services.Core;
    using Fanex.Affiliate.Services.Core.Configuration;
    using Fanex.Data;
    using Fanex.Data.DNAide;

    /// <summary>
    /// Base DataAccess.
    /// </summary>
    public abstract class BaseDataAccess
    {
        /// <summary>
        /// Default time out when executing a stored procedure.
        /// </summary>
        private const int DefaultTimeOut = 600;

        /// <summary>
        /// The licensee.
        /// </summary>
        private readonly Licensee _licensee;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDataAccess"/> class.
        /// </summary>
        /// <param name="licensee">The licensee.</param>
        protected BaseDataAccess(Licensee licensee)
        {
            _licensee = licensee;
        }

        /// <summary>
        /// Base method.
        /// </summary>
        /// <param name="uspKey">The USP Key.</param>
        /// <param name="parameters">The SP parameters.</param>
        /// <param name="timeOut">The SP executing timeout.</param>
        /// <returns>Return DB result.</returns>
        protected DbResult ExecSp(string uspKey, Hashtable parameters, int timeOut)
        {
            var actualKey = GetSpKeyByLicensee(uspKey);
            using (var objectDb = new ObjectDb(actualKey))
            {
                var db = objectDb.Database;
                var command = objectDb.DbCommand;
                command.CommandTimeout = timeOut;
                objectDb.AssignParamValues(parameters);
                DataSet ds = db.ExecuteDataSet(command);
                Hashtable hashParams = objectDb.GetOuputParams();
                return new DbResult(hashParams, ds);
            }
        }

        /// <summary>
        /// Execute Store procedure.
        /// </summary>
        /// <param name="uspKey">The SP key.</param>
        /// <param name="parameters">The SP parameters.</param>
        /// <returns>Return DB result.</returns>
        protected DbResult ExecSp(string uspKey, Hashtable parameters)
        {
            return ExecSp(uspKey, parameters, DefaultTimeOut);
        }

        /// <summary>
        /// Return DataTable.
        /// </summary>
        /// <param name="uspKey">The USP Key.</param>
        /// <param name="parameters">The SP parameters.</param>
        /// <returns>Returns DataTable.</returns>
        protected DataTable ExecSpDataTable(string uspKey, Hashtable parameters)
        {
            var result = ExecSp(uspKey, parameters);
            if (result != null && result.ResultSet.Tables.Count > 0)
            {
                return result.ResultSet.Tables[0];
            }

            return null;
        }

        /// <summary>
        /// Return DataSet.
        /// </summary>
        /// <param name="uspKey">The USP Key.</param>
        /// <param name="parameters">The SP parameters.</param>
        /// <returns>Returns DataSet.</returns>
        protected DataSet ExecSpDataSet(string uspKey, Hashtable parameters)
        {
            var result = ExecSp(uspKey, parameters);
            return result.ResultSet;
        }

        /// <summary>
        /// Return HashTable.
        /// </summary>
        /// <param name="uspKey">The USP Key.</param>
        /// <param name="parameters">The SP parameters.</param>
        /// <returns>
        /// Returns HashTable.
        /// </returns>
        protected Hashtable ExecSpHashtable(string uspKey, Hashtable parameters)
        {
            var result = ExecSp(uspKey, parameters);
            if (result != null && result.Outputs != null && result.Outputs.Count > 0)
            {
                return result.Outputs;
            }

            return null;
        }

        /// <summary>
        /// Return DataReader.
        /// </summary>
        /// <param name="uspKey">The USP Key.</param>
        /// <param name="parameters">The SP parameters.</param>
        /// <returns>Returns DataReader.</returns>
        protected IDataReader ExecSpDataReader(string uspKey, Hashtable parameters)
        {
            var actualKey = GetSpKeyByLicensee(uspKey);
            var objectDb = new ObjectDb(actualKey);
            var db = objectDb.Database;
            var command = objectDb.DbCommand;
            objectDb.AssignParamValues(parameters);
            return db.ExecuteReader(command);
        }

        /// <summary>
        /// Return DataReader.
        /// </summary>
        /// <param name="uspKey">The USP Key.</param>
        /// <returns>Return IDataReader.</returns>
        protected IDataReader ExecSpDataReader(string uspKey)
        {
            return ExecSpDataReader(uspKey, null);
        }

        /// <summary>
        /// Write Log by exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        protected void ProcessException(Exception exception)
        {
            Logger.Error(exception);
            throw exception;
        }

        /// <summary>
        /// Return actual USP key for licensee.
        /// For example: MYMBET_OriginalKey, AONCASH_OriginalKey,..
        /// </summary>
        /// <param name="uspKey">The original key.</param>
        /// <returns>Actual key.</returns>
        protected string GetSpKeyByLicensee(string uspKey)
        {
            return uspKey.StartsWith("common_", StringComparison.CurrentCultureIgnoreCase)
            ? uspKey
            : _licensee.Name.ToUpper() + "_" + uspKey;
        }
    }
}