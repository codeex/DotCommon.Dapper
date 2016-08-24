using System.Linq.Expressions;
using DotCommon.Dapper.Extensions;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class SqlServerSelectTranslator : SqlServerQueryTranslator
    {
	    private readonly MemberAliasMapContainer _mapContainer;

	    public SqlServerSelectTranslator(TranslatorDelegate translatorDelegate)
		    : base(translatorDelegate)
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
			//判断是否为多表操作,如果是多表操作则设置
	        bool multiple = expr.Parameters.Count > 0;
		    TranslatorDelegate.SetMultipleType();

            foreach (var item in _mapContainer.MemberAliasMaps)
	        {
	            if (multiple)
	            {
	                SqlBuilder.Append($"[{TranslatorDelegate.GetTypeAlias(item.PropInfo.Type)}].");
	            }
	            SqlBuilder.Append($"[{TranslatorDelegate.GetPropMap(item.PropInfo)}] AS [{item.Alias}],");
	        }
	        SqlBuilder.Remove(SqlBuilder.Length - 1, 1);
		    return SqlBuilder.ToString();
	    }

	    protected override Expression VisitMember(MemberExpression node)
	    {
		    _mapContainer.SetPropInfo(node.Member);
		    return base.VisitMember(node);
	    }

	    protected override MemberBinding VisitMemberBinding(MemberBinding node)
	    {
		    _mapContainer.SetAlias(node.Member.Name);
		    return base.VisitMemberBinding(node);
	    }
    }
}
