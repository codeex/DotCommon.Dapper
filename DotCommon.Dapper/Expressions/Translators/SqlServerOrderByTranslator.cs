using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{

    public class SqlServerOrderByTranslator : SqlServerQueryTranslator
    {
        private OrderBySectionParameter _parameter;
        public SqlServerOrderByTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter)
            : base(translatorDelegate)
        {
            _parameter = (OrderBySectionParameter) parameter;
        }

        public override string Translate(LambdaExpression expr)
        {
            Visit(expr.Body);
            return SqlBuilder.ToString();
        }

	    protected override Expression VisitMember(MemberExpression node)
	    {
		    var asc = _parameter.IsAsc ? "ASC" : "DESC";
		    SqlBuilder.Append(TranslatorDelegate.IsFirstVisit(SectionType.OrderBy) ? $"," : $" ORDER BY");
		    if (TranslatorDelegate.IsMultipleType())
		    {
			    SqlBuilder.Append($" [{TranslatorDelegate.GetTypeAlias(node.Member.DeclaringType)}].");
		    }
		    SqlBuilder.Append($"[{TranslatorDelegate.GetMemberMap(node.Member)}] {asc}");
		    return base.VisitMember(node);
	    }
    }
}
