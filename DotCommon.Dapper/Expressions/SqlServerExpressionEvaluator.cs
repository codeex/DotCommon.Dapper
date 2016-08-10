using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DotCommon.Dapper.Common;
using DotCommon.Dapper.Expressions.Builder;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions
{
    public class SqlServerExpressionEvaluator : BaseExpressionEvaluator
    {
        private readonly ConcurrentDictionary<int, string> _sqlDict = new ConcurrentDictionary<int, string>();

        /// <summary>解析表达式
        /// </summary>
        public string EvalQuery(QueryWapper queryWapper)
        {
            Ensure.NotNull(queryWapper, "QueryWapper");

            var builder = (SqlServerQueryBuilder) SqlBuilderFactory.CreateQueryBuilder(SqlType.SqlServer, queryWapper);


            return "";
        }




    }
}
