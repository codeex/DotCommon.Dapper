using System.Linq.Expressions;
using System.Text;

namespace DotCommon.Dapper.Expressions.Translators
{
    public abstract class SqlServerTranslator : ExpressionVisitor, ITranslator
    {
        protected StringBuilder SqlBuilder { get; set; } = new StringBuilder();
        public SqlType SqlType { get; } = SqlType.SqlServer;
        public abstract string Translate(LambdaExpression expr);
    }
}
