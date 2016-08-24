using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;
using DotCommon.Dapper.Extensions;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class MySqlOrderByTranslator:MySqlQueryTranslator
    {
        public MySqlOrderByTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter) : base(translatorDelegate,parameter)
        {
        }
        public override string Translate(LambdaExpression expr)
        {
            Visit(expr.Body);
            return SqlBuilder.ToString();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var asc = ((OrderBySectionParameter)Parameter).IsAsc ? "ASC" : "DESC";
            SqlBuilder.Append(TranslatorDelegate.IsFirstVisit(SectionType.OrderBy) ? $"," : $" ORDER BY");
			if (TranslatorDelegate.IsMultipleType())
			{
                SqlBuilder.Append($" [{TranslatorDelegate.GetTypeAlias(node.Member.DeclaringType)}].");
            }
            SqlBuilder.Append($"[{TranslatorDelegate.GetPropMap(node.Member.ToProp())}] {asc}");
            return base.VisitMember(node);
        }
    }
}
