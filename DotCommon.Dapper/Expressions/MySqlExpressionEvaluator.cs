using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotCommon.Dapper.Common;
using DotCommon.Dapper.Expressions.Builder;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions
{
    public class MySqlExpressionEvaluator: BaseExpressionEvaluator
    {
        /// <summary>解析表达式
        /// </summary>
        public string EvalQuery(QueryWapper queryWapper)
        {
            Ensure.NotNull(queryWapper, "QueryWapper");
            var builder = (MySqlQueryBuilder) SqlBuilderFactory.CreateQueryBuilder(SqlType.MySql, queryWapper);

            return "";
        }

    }
}
