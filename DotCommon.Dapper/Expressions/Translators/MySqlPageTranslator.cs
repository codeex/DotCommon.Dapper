using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class MySqlPageTranslator:MySqlQueryTranslator
    {
        private readonly string _querySql;
        public MySqlPageTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter,string querySql) : base(translatorDelegate,parameter)
        {
            _querySql = querySql;
        }

        public override string Translate(LambdaExpression expr)
        {
            var parameter = (PageSectionParameter) Parameter;
            SqlBuilder.Append(
                $"{_querySql} LIMIT {(parameter.PageIndex - 1)*parameter.PageSize},{parameter.PageSize}");
            return SqlBuilder.ToString();
        }
    }
}
