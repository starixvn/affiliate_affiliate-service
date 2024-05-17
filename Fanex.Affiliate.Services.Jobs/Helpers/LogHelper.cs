using Fanex.Data;
using Fanex.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Text;

namespace Fanex.Affiliate.Services.Jobs.Helpers
{
    public static class LogHelper
    {
        private const int LimitedLevelOfInnerException = 3;

        public static void InfoImport(string jobTitle, DateTime date)
        {
            LogService.Info($"{jobTitle} - date {date}");
        }

        public static void Error(Exception exception, string additionalMessage = null)
        {
            var content = new StringBuilder().AppendLine($"Error Message: {exception?.Message}");
            if (!string.IsNullOrWhiteSpace(additionalMessage))
            {
                content.AppendLine(additionalMessage);
            }

            AddDatabaseInfo(content, exception);
            AddExceptionInfo(content, exception, LimitedLevelOfInnerException);

            LogService.Error(content.ToString(), exception);
        }

        private static void AddDatabaseInfo(StringBuilder builder, Exception exception)
        {
            builder.AppendLine();
            builder.AppendLine("DATABASE INFO");

            var dbInfo = GetDbExceptionInfo(exception);

            foreach (var item in dbInfo)
            {
                builder.AppendLine($"{item.Key}: {item.Value}");
            }
        }

        private static IDictionary<string, object> GetDbExceptionInfo(Exception exception)
        {
            var dbInfo = new Dictionary<string, object>();
            BindSqlExceptionInfo(exception, dbInfo);
            BindFanexDataExceptionInfo(exception, dbInfo);
            return dbInfo;
        }

        private static void BindFanexDataExceptionInfo(Exception exception, IDictionary<string, object> dbInfo)
        {
            if (exception is ObjectDbException fanexDataException)
            {
                BindSqlExceptionInfo(exception.InnerException, dbInfo);

                var connString = new SqlConnectionStringBuilder(fanexDataException.DbSetting.ConnectionString);

                dbInfo["Server"] = connString.DataSource;
                dbInfo["DbName"] = fanexDataException.DbSetting.DbName;
                dbInfo["SpName"] = fanexDataException.DbSetting.SpName;
                dbInfo["UserId"] = connString.UserID;
                dbInfo["CommandTimeout"] = fanexDataException.DbSetting.ExecuteTimeOut;
            }
        }

        private static void BindSqlExceptionInfo(Exception exception, IDictionary<string, object> dbInfo)
        {
            if (exception is SqlException sqlException)
            {
                dbInfo["Server"] = sqlException.Server;
                dbInfo["Procedure"] = sqlException.Procedure;
                dbInfo["Line"] = sqlException.LineNumber;
            }
        }

        private static void AddExceptionInfo(StringBuilder builder, Exception exception, int limitedInnerException)
        {
            builder.AppendLine();
            builder.AppendLine($"EXCEPTION INFO LEVEL: {LimitedLevelOfInnerException - limitedInnerException}");
            builder.AppendLine($"Message: {exception.Message}");
            builder.AppendLine($"Source: {exception.Source}");
            builder.AppendLine($"TargetSite: {exception.TargetSite}");
            builder.AppendLine($"StackTrace: {exception.StackTrace}");

            if (exception.InnerException != null && limitedInnerException > 0)
            {
                AddExceptionInfo(builder, exception.InnerException, --limitedInnerException);
            }

            if (exception is SocketException)
            {
                var socketEx = exception as SocketException;
                builder.AppendLine();
                builder.AppendLine($"SOCKET EXCEPTION INFO");
                builder.AppendLine($"Sockets Error Codes: {socketEx.SocketErrorCode}");
                builder.AppendLine($"Native Error Codes: {socketEx.NativeErrorCode}");
                builder.AppendLine($"Error Codes: {socketEx.ErrorCode}");
            }
        }
    }
}
