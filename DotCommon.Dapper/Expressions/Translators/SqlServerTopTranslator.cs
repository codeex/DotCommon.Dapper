using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class SqlServerTopTranslator:SqlServerQueryTranslator
    {
        private TopSectionParameter _parameter;

        public SqlServerTopTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter)
            : base(translatorDelegate)
        {
            _parameter = (TopSectionParameter) parameter;
        }

        public override string Translate(LambdaExpression expr)
        {
            Visit(expr.Body);
            SqlBuilder.Append($" TOP {_parameter.Top}");
            return SqlBuilder.ToString();
        }


    }
}
