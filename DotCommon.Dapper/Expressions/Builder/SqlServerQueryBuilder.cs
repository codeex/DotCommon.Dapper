using System.Collections.Generic;
using System.Text;
using Dapper;
using DotCommon.Dapper.Common;
using DotCommon.Dapper.Expressions.Sections;
using DotCommon.Dapper.Expressions.Translators;

namespace DotCommon.Dapper.Expressions.Builder
{
    public class SqlServerQueryBuilder : BaseQueryBuilder
    {
        public SqlServerQueryBuilder(QueryWapper queryWapper) : base(SqlType.SqlServer, queryWapper)
        {
            SqlBuilder = new StringBuilder();
            TranslatorDelegate = new TranslatorDelegate(GetTableName, GetMapName, GetTypeAlias, SectionIsMultiple,
                GetIsMultipleType, SetMultipleType, IsFirstVisit, SetVisited);
        }



        /// <summary>构建查询语句
        /// </summary>
        public void BuildQuery(out string sql, out DynamicParameters parameters)
        {
            var selectSection = QueryWapper.FindSection(SectionType.Select);
            var whereSection = QueryWapper.FindSection(SectionType.Where);
            var orderBySection = QueryWapper.FindSection(SectionType.OrderBy);
            var joinBySection = QueryWapper.FindSection(SectionType.Join);
            var groupBySection = QueryWapper.FindSection(SectionType.GroupBy);
            var havingSection = QueryWapper.FindSection(SectionType.Having);
            Ensure.NotNull(selectSection, "select");

            SqlBuilder.Append($"SELECT");
            if (groupBySection != null)
            {

            }
            else
            {

            }

            sql = "";
            parameters = default(DynamicParameters);
        }
















    }
}
