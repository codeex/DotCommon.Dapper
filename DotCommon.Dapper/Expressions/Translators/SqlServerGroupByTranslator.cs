using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DotCommon.Dapper.Expressions.Translators
{
	public class SqlServerGroupByTranslator:SqlServerQueryTranslator
	{
		private readonly MemberAliasMapContainer _mapContainer;
		private readonly StringBuilder _groupByBuilder;

		public SqlServerGroupByTranslator(TranslatorDelegate translatorDelegate) : base(translatorDelegate)
		{
			_mapContainer = new MemberAliasMapContainer();
			_groupByBuilder = new StringBuilder();
		}

	    private void AppendMethodSql(PropInfo propInfo, MemberAliasItem item)
	    {
            _groupByBuilder.Append($" ");
	        var funcName = "";

	        switch (propInfo.KeyName)
	        {
	            case "SqlCount":
	                funcName = "COUNT";
	                break;
	            case "SqlSum":
	                funcName = "SUM";
	                break;
	            case "SqlAvg":
	                funcName = "AVG";
	                break;
	        }

	        _groupByBuilder.Append($"{funcName}(");
            if (TranslatorDelegate.IsMultipleType())
            {
                _groupByBuilder.Append($"[{TranslatorDelegate.GetTypeAlias(item.PropInfo.Type)}].");
            }
            _groupByBuilder.Append($"[{TranslatorDelegate.GetPropMap(item.PropInfo)}])");
            _groupByBuilder.Append($" AS [{item.Alias}]");
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
	        SqlBuilder.Append($" GROUP BY");
	        foreach (var item in _mapContainer.MemberAliasMaps)
	        {
	            if (!item.PropInfo.IsNull())
	            {
	                var keyName = item.PropInfo.KeyName;
                    //如果是扩展的几个方法,则生成的Sql语句会不同
	                if (keyName == "SqlCount" || keyName == "SqlSum" || keyName == "SqlAvg")
	                {
	                    AppendMethodSql(item.PropInfo, item);
	                    continue;
	                }
	                _groupByBuilder.Append($" ");
                    SqlBuilder.Append($" ");
                    if (TranslatorDelegate.IsMultipleType())
	                {
	                    _groupByBuilder.Append($"[{TranslatorDelegate.GetTypeAlias(item.PropInfo.Type)}].");
	                    SqlBuilder.Append($"[{TranslatorDelegate.GetTypeAlias(item.PropInfo.Type)}].");
	                }
	                _groupByBuilder.Append($"[{TranslatorDelegate.GetPropMap(item.PropInfo)}] AS [{item.Alias}],");
	                SqlBuilder.Append($"[{TranslatorDelegate.GetPropMap(item.PropInfo)}],");
	            }
	        }
	        if (_groupByBuilder.Length > 0)
	        {
	            _groupByBuilder.Remove(_groupByBuilder.Length - 1, 1);
	        }
	        if (SqlBuilder.Length > 0)
	        {
	            SqlBuilder.Remove(SqlBuilder.Length - 1, 1);
	        }
	        return SqlBuilder.ToString();
	    }

	    protected override Expression VisitMember(MemberExpression node)
        {
            _mapContainer.SetPropInfo(node.Member);
            return base.VisitMember(node);
        }

	    protected override Expression VisitMethodCall(MethodCallExpression node)
	    {
	        if (node.Method.Name == "SqlCount" || node.Method.Name == "SqlSum" || node.Method.Name == "SqlAvg")
	        {
	            MemberInfo member=default(MemberInfo);
	            if (node.Arguments[0] is UnaryExpression)
	            {
	                var unaryExpr = node.Arguments[0] as UnaryExpression;
	                var memberExpr = unaryExpr?.Operand as MemberExpression;
	                member = memberExpr?.Member;
	            }
	            if (node.Arguments[0] is MemberExpression)
	            {
	                var memberExpr = node.Arguments[0] as MemberExpression;
	                member = memberExpr?.Member;
	            }

	            _mapContainer.SetPropInfo(new PropInfo(member?.DeclaringType, member?.Name,
	                node.Method.Name));
	            return node;
	        }
	        return base.VisitMethodCall(node);
	    }

	    public string GetGroupBySelect()
		{
			return _groupByBuilder.ToString();
		}
	}
}
