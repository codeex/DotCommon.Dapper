using System.Linq.Expressions;
using System.Text;

namespace DotCommon.Dapper.Expressions.Translators
{
	public class MySqlGroupByTranslator:MySqlQueryTranslator
	{
		private readonly MemberAliasMapContainer _mapContainer;
		private readonly StringBuilder _groupByBuilder;
		public MySqlGroupByTranslator(TranslatorDelegate translatorDelegate) : base(translatorDelegate)
		{
			_mapContainer = new MemberAliasMapContainer();
			_groupByBuilder =new StringBuilder();
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
				if (TranslatorDelegate.IsMultipleType())
				{
					_groupByBuilder.Append($"`{TranslatorDelegate.GetTypeAlias(item.Member.DeclaringType)}`.");
				}
				_groupByBuilder.Append($"`{TranslatorDelegate.GetMemberMap(item.Member)}`,");
			}
			_groupByBuilder.Remove(SqlBuilder.Length - 1, 1);
			SqlBuilder.Append(_groupByBuilder);
			return SqlBuilder.ToString();
		}
		public string GetGroupBySelect()
		{
			return _groupByBuilder.ToString();
		}
	}
}
