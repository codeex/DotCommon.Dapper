using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class SqlServerTopTranslator:SqlServerQueryTranslator
    {


        public SqlServerTopTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter)
            : base(translatorDelegate, parameter)
        {
        }

        public override string Translate(LambdaExpression expr)
        {
            Visit(expr.Body);
            SqlBuilder.Append($" TOP {((TopSectionParameter) Parameter).Top}");
            return SqlBuilder.ToString();
        }


    }
}
