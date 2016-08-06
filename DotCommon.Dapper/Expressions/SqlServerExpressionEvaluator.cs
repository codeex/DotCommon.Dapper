using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DotCommon.Dapper.Common;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions
{
    public class SqlServerExpressionEvaluator:BaseExpressionEvaluator
    {

        /// <summary>解析表达式
        /// </summary>
        public string EvalQuery(List<BaseSection> sections)
        {
            SetSections(sections);
            return "";
        }

        /// <summary>构建Select查询
        /// </summary>
        private string BuildSelect()
        {
            var lambda = GetSelectExpression();
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
