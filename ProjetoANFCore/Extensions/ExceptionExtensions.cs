using System;
using System.Data.SqlClient;
using System.Text;

namespace ProjetoANFCore.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetAllMessages(this Exception exception)
        {
            Exception e = exception;
            StringBuilder s = new StringBuilder();
            while (e != null)
            {
                s.AppendLine("Exception type: " + e.GetType().FullName);
                s.AppendLine("Message       : " + e.Message);
                s.AppendLine("Stacktrace:");
                s.AppendLine(e.StackTrace);
                s.AppendLine();
                e = e.InnerException;
            }
            return s.ToString();
        }

        public static SqlException SqlException(this Exception exception)
        {
            SqlException sqlEx = null;

            while (exception != null)
            {
                if (exception.GetType() != typeof(SqlException))
                {
                    exception = exception.InnerException;
                    continue;
                }

                sqlEx = (exception as SqlException);
                exception = null;
            }
            return sqlEx;
        }
    }
}
