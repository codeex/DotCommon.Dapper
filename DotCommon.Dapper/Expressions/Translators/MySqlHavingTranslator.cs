using System.Linq.Expressions;
using System.Reflection;
using Dapper;
using DotCommon.Dapper.Extensions;

namespace DotCommon.Dapper.Expressions.Translators
{
	public class MySqlHavingTranslator:MySqlQueryTranslator
	{
        private readonly DynamicParameters _parameters;
        private int _paramIndex = 0;
        public MySqlHavingTranslator(TranslatorDelegate translatorDelegate) : base(translatorDelegate)
		{
            _parameters = new DynamicParameters();
        }

		public override string Translate(LambdaExpression expr)
		{
            SqlBuilder.Append($" HAVING");
            Visit(expr.Body);
            return SqlBuilder.ToString();
        }
        private string GetParameterName()
        {
            _paramIndex++;
            return $"@param_hv_{_paramIndex}";
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Visit(node.Left);
            switch (node.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    SqlBuilder.Append(" AND ");
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    SqlBuilder.Append(" OR ");
                    break;
                case ExpressionType.Equal:
                    SqlBuilder.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    SqlBuilder.Append(" <> ");
                    break;
                case ExpressionType.LessThan:
                    SqlBuilder.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    SqlBuilder.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    SqlBuilder.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    SqlBuilder.Append(" >= ");
                    break;
            }
            Visit(node.Right);
            return node;
        }

        private string GetFunctionName(string methodName)
        {
            var funcName = "";
            switch (methodName)
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
            return funcName;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var methodName = node.Method.Name;
            if (methodName == "SqlCount" || methodName == "SqlSum" || methodName == "SqlAvg")
            {
                SqlBuilder.Append($" {GetFunctionName(methodName)}(");
                var member = default(MemberInfo);
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
                if (TranslatorDelegate.IsMultipleType())
                {
                    SqlBuilder.Append($"`{TranslatorDelegate.GetTypeAlias(member?.DeclaringType)}`.");
                }
                SqlBuilder.Append($"`{TranslatorDelegate.GetPropMap(member.ToProp())}`)");
                return node;
            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            SqlBuilder.Append($" ");
            if (TranslatorDelegate.IsMultipleType())
            {
                SqlBuilder.Append($"`{TranslatorDelegate.GetTypeAlias(node.Member.DeclaringType)}`.");
            }
            SqlBuilder.Append($"`{TranslatorDelegate.GetPropMap(node.Member.ToProp())}`");
            return base.VisitMember(node);
        }

	    protected override Expression VisitConstant(ConstantExpression node)
	    {
	        var paramName = GetParameterName();
	        SqlBuilder.Append(paramName);
	        _parameters.Add(paramName, node.Value);
	        return node;
	    }
	}
}
