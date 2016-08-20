using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Builder
{
    public class MySqlQueryBuilder : BaseQueryBuilder
    {
        public MySqlQueryBuilder(QueryWapper queryWapper) : base(SqlType.MySql, queryWapper)
        {
            QueryWapper = queryWapper;
        }


    }
}
