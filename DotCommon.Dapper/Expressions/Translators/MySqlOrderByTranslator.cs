using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class MySqlOrderByTranslator:MySqlQueryTranslator
    {
        private OrderBySectionParameter _parameter;
        private bool _multiple = false;
        public MySqlOrderByTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter) : base(translatorDelegate)
        {
            _parameter = (OrderBySectionParameter)parameter;
        }
        public override string Translate(LambdaExpression expr)
        {
            Visit(expr.Body);
            return SqlBuilder.ToString();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var asc = _parameter.IsAsc ? "ASC" : "DESC";
            SqlBuilder.Append(TranslatorDelegate.GetTypeOneMore(OneMoreType.OrderBy) ? $"," : $" ORDER BY");
            if (_multiple)
            {
                SqlBuilder.Append($" [{TranslatorDelegate.GetTypeAlias(node.Member.DeclaringType)}].");
            }
            SqlBuilder.Append($"[{TranslatorDelegate.GetMemberMapDelegate(node.Member)}] {asc}");
            return base.VisitMember(node);
        }
    }
}
