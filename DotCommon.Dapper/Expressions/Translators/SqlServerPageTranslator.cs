using System;
using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class SqlServerPageTranslator:SqlServerQueryTranslator
    {

        private readonly string _orderBySql;
        private readonly string _querySql;

        public SqlServerPageTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter,
            string orderBySql, string querySql)
            : base(translatorDelegate,parameter)
        {
            _orderBySql = orderBySql;
            _querySql = querySql;
        }

        public override string Translate(LambdaExpression expr)
        {
            if (string.IsNullOrWhiteSpace(_orderBySql))
            {
                throw new ArgumentException("Order by is must in SqlServer paging.");
            }
            var parameter = (PageSectionParameter) Parameter;
            SqlBuilder.Append(
                $"SELECT * FROM (SELECT ROW_NUMBER() OVER ({_orderBySql}) AS RowNumber, {_querySql}) AS Total WHERE RowNumber >= {(parameter.PageIndex - 1)*parameter.PageSize + 1} AND RowNumber <= {parameter.PageIndex*parameter.PageSize}");
            return SqlBuilder.ToString();
        }
    }
}
