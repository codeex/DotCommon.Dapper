using System;
using System.Collections.Generic;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Builder
{
    public class SqlServerQueryBuilder : BaseQueryBuilder
    {
        public List<Tuple<SectionType, Func<string>>> SectionHandlerDict = new List<Tuple<SectionType, Func<string>>>();

        public SqlServerQueryBuilder(SqlType sqlType, QueryWapper queryWapper) : base(sqlType,queryWapper)
        {

        }

        /// <summary>构建查询语句
        /// </summary>
        public string BuildQuery()
        {
            foreach (var sectionHandler in SectionHandlerDict)
            {

            }
            return "";
        }


        












    }
}
