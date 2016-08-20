using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Builder
{
    public class SqlServerQueryBuilder : BaseQueryBuilder
    {
        public List<Tuple<SectionType, Func<string>>> SectionHandlerDict = new List<Tuple<SectionType, Func<string>>>();

        public SqlServerQueryBuilder(QueryWapper queryWapper) : base(SqlType.SqlServer, queryWapper)
        {

        }

        /// <summary>构建查询语句
        /// </summary>
        public string BuildQuery()
        {
            foreach (var sectionHandler in SectionHandlerDict)
            {
                var select = "SELECT a.Id AS AID,a.K AS AK FROM T1 a INNER JOIN T2 b ON a.Id=b.Cid";
            }
            return "";
        }

        private string BuildSelectColumns()
        {
            var sqlBuilder = new StringBuilder();
            var selectTypes = GetSelectTypes();
            //Select 中的参数为null
            if (selectTypes == null || !selectTypes.Any())
            {

            }
            else
            {
                
            }


            return sqlBuilder.ToString();
        }
















    }
}
