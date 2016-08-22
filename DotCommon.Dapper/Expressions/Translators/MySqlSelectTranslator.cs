using System.Linq.Expressions;
using System.Text;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class MySqlSelectTranslator :MySqlQueryTranslator
    {
        public MySqlSelectTranslator(TranslatorDelegate translatorDelegate) : base(translatorDelegate)
        {
        }
        public override string Translate(LambdaExpression expr)
        {
            Visit(expr);
            return SqlBuilder.ToString();
        }

        protected override Expression VisitNew(NewExpression node)
        {
            
            return base.VisitNew(node);
        }

      
    }
}
