using System.Data;
using DotCommon.Dapper.Expressions;

namespace DotCommon.Dapper.Extensions
{
    public static class DbConnectionExtensions
    {
        public static SchemaContext Schema(this IDbConnection connection)
        {
            return new SchemaContext(connection);
        }
    }
}
