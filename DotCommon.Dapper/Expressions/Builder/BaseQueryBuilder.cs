using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Builder
{
    public abstract class BaseQueryBuilder : IQueryBuilder
    {
        public SqlType SqlType { get; protected set; }

        public QueryWapper QueryWapper { get; protected set; }

        protected BaseQueryBuilder(SqlType sqlType, QueryWapper queryWapper)
        {
            SqlType = sqlType;
        }
    }
}
