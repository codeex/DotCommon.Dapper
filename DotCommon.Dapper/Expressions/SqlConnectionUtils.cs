using System.Collections.Generic;
using System.Data;

namespace DotCommon.Dapper.Expressions
{
    public class SqlConnectionUtils
    {
        private static readonly Dictionary<string, SqlType> SqlTypeDict = new Dictionary
            <string, SqlType>()
        {
            {"sqlconnection", SqlType.SqlServer},
            {"mysqlconnection", SqlType.MySql}
        };

        private static readonly Dictionary<string, IExpressionEvaluator> EvaliatorDict = new Dictionary
            <string, IExpressionEvaluator>()
        {
            {"sqlconnection", new SqlServerExpressionEvaluator()},
            {"mysqlconnection", new MySqlExpressionEvaluator()}
        };

        private static string GetName(IDbConnection connection)
        {
            return connection.GetType().Name.ToLower();
        }

        public static SqlType GetSqlType(IDbConnection connection)
        {
            string name = GetName(connection);
            return SqlTypeDict.ContainsKey(name) ? SqlTypeDict[name] : SqlType.SqlServer;
        }

        public static IExpressionEvaluator GetEvaluator(IDbConnection connection)
        {
            string name = GetName(connection);
            return EvaliatorDict.ContainsKey(name) ? EvaliatorDict[name] : new SqlServerExpressionEvaluator();
        }

    }
}
