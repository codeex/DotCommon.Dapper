using System.Linq.Expressions;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class MySqlSelectTranslator :MySqlQueryTranslator
    {
        private readonly MemberAliasMapContainer _mapContainer;
        public MySqlSelectTranslator(TranslatorDelegate translatorDelegate) : base(translatorDelegate)
        {
            _mapContainer = new MemberAliasMapContainer();
        }
        public override string Translate(LambdaExpression expr)
        {
            var newExpr = expr.Body as NewExpression;
            if (newExpr != null)
            {
                foreach (var member in newExpr.Members)
                {
                    _mapContainer.SetAlias(member.Name);
                }
            }
            Visit(expr.Body);
            bool multiple = expr.Parameters.Count > 0;
            foreach (var item in _mapContainer.MemberAliasMaps)
            {
                if (multiple)
                {
                    SqlBuilder.Append($"`{TranslatorDelegate.GetTypeAlias(item.Member.DeclaringType)}`.");
                }
                SqlBuilder.Append($"`{TranslatorDelegate.GetMemberMapDelegate(item.Member)}] AS [{item.Alias}`,");
            }
            SqlBuilder.Remove(SqlBuilder.Length - 1, 1);
            return SqlBuilder.ToString();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _mapContainer.SetMember(node.Member);
            return base.VisitMember(node);
        }

        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            _mapContainer.SetAlias(node.Member.Name);
            return base.VisitMemberBinding(node);
        }


    }
}
