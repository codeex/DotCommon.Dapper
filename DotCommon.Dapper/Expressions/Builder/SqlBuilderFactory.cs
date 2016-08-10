using System.Collections.Generic;
using DotCommon.Dapper.Common;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Builder
{
    public class SqlBuilderFactory
    {

        public static IQueryBuilder CreateQueryBuilder(SqlType sqlType, QueryWapper queryWapper)
        {
            if (sqlType == SqlType.MySql)
            {
                return new MySqlQueryBuilder(sqlType,queryWapper);
            }
            return new SqlServerQueryBuilder(sqlType, queryWapper);
        }


    }
}
