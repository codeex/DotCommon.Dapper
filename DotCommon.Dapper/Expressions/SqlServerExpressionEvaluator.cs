using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DotCommon.Dapper.Common;

namespace DotCommon.Dapper.Expressions
{
    public class SqlServerExpressionEvaluator:BaseExpressionEvaluator
    {
	    private  ConcurrentDictionary<int, string> _sqlDict = new ConcurrentDictionary<int, string>();

	    /// <summary>解析表达式
	    /// </summary>
	    public string EvalQuery(QueryWapper queryWapper)
	    {
		    var hash = queryWapper.GetHashCode();
		    var sql = "";
		    if (_sqlDict.TryGetValue(hash, out sql))
		    {
			    return sql;
		    }
		    var sqlBuilder = new StringBuilder();


		    return sqlBuilder.ToString();
	    }

	    /// <summary>构建Select查询
        /// </summary>
        private string BuildSelect()
        {

            var sb = new StringBuilder();
            sb.AppendFormat("SELECT  ");
            
            return "";
        }

        /// <summary>构建Where 条件查询
        /// </summary>
        private string BuildWhere()
        {

            return "";
        }

        /// <summary>构建OrderBy 查询
        /// </summary>
        private string BuildOrderBy()
        {

            return "";
        }









    }
}
