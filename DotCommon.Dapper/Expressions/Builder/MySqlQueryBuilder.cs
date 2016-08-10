using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Builder
{
    public class MySqlQueryBuilder : BaseQueryBuilder
    {
        public MySqlQueryBuilder(SqlType sqlType, QueryWapper queryWapper) : base(sqlType, queryWapper)
        {
            QueryWapper = queryWapper;
        }


    }
}
