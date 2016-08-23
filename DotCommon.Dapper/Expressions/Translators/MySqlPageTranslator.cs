using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class MySqlPageTranslator:MySqlQueryTranslator
    {
        private PageSectionParameter _parameter;
        private readonly string _querySql;
        public MySqlPageTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter,string querySql) : base(translatorDelegate)
        {
            _parameter = (PageSectionParameter)parameter;
            _querySql = querySql;
        }

        public override string Translate(LambdaExpression expr)
        {
            SqlBuilder.Append(
                $"{_querySql} LIMIT {(_parameter.PageIndex - 1)*_parameter.PageSize},{_parameter.PageSize}");
            return SqlBuilder.ToString();
        }
    }
}
