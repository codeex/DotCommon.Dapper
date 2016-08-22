using System.Linq.Expressions;
using System.Text;

namespace DotCommon.Dapper.Expressions.Translators
{
    public abstract class MySqlTranslator : ExpressionVisitor, ITranslator
    {
        protected StringBuilder SqlBuilder { get; set; } = new StringBuilder();
        public SqlType SqlType { get; } = SqlType.MySql;
        public abstract string Translate(Expression expr);
    }
}
