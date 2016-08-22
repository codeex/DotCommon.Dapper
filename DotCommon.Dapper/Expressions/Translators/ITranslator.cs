using System.Linq.Expressions;

namespace DotCommon.Dapper.Expressions.Translators
{
    public interface ITranslator
    {
        SqlType SqlType { get; }
        string Translate(LambdaExpression expr);

    }
}
