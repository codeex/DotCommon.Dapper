using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class MySqlTopTranslator:MySqlQueryTranslator
    {
        private TopSectionParameter _parameter;

        public MySqlTopTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter)
            : base(translatorDelegate)
        {
            _parameter = (TopSectionParameter) parameter;
        }

        public override string Translate(LambdaExpression expr)
        {
            Visit(expr.Body);
            SqlBuilder.Append($" LIMIT {_parameter.Top}");
            return SqlBuilder.ToString();
        }
    }
}
