using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class SqlServerPageTranslator:SqlServerQueryTranslator
    {
        private PageSectionParameter _parameter;
        private readonly string _orderBySql;
        private readonly string _querySql;

        public SqlServerPageTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter,
            string orderBySql, string querySql)
            : base(translatorDelegate)
        {
            _parameter = (PageSectionParameter) parameter;
            _orderBySql = orderBySql;
            _querySql = querySql;
        }

        public override string Translate(LambdaExpression expr)
        {
            SqlBuilder.Append(
                $"SELECT * FROM (SELECT ROW_NUMBER() OVER ({_orderBySql}) AS RowNumber, {_querySql}) AS Total WHERE RowNumber >= {(_parameter.PageIndex - 1)*_parameter.PageSize + 1} AND RowNumber <= {_parameter.PageIndex*_parameter.PageSize}");
            return SqlBuilder.ToString();
        }
    }
}
