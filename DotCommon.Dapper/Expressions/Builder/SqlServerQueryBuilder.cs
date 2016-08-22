using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DotCommon.Dapper.Expressions.Sections;
using DotCommon.Dapper.FluentMap;

namespace DotCommon.Dapper.Expressions.Builder
{
    public class SqlServerQueryBuilder : BaseQueryBuilder
    {
        public SqlServerQueryBuilder(QueryWapper queryWapper) : base(SqlType.SqlServer, queryWapper)
        {

        }

        /// <summary>构建查询语句
        /// </summary>
        public string BuildQuery()
        {

            return "";
        }

        private string BuildSelectColumns()
        {
            var sqlBuilder = new StringBuilder();
           


            return sqlBuilder.ToString();
        }
















    }
}
