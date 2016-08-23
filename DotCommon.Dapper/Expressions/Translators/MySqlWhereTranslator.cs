using System.Linq.Expressions;
using Dapper;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class MySqlWhereTranslator:MySqlQueryTranslator
    {
        private readonly DynamicParameters _parameters;
        private int _paramIndex = 0;
        public MySqlWhereTranslator(TranslatorDelegate translatorDelegate) : base(translatorDelegate)
        {
            _parameters = new DynamicParameters();
        }
        public DynamicParameters GetParameters()
        {
            return _parameters;
        }

        public override string Translate(LambdaExpression expr)
        {
			//判断该Where查询是否为第一次访问,如果是第一次访问,则设置访问
			if (TranslatorDelegate.IsFirstVisit(SectionType.Where))
			{
				SqlBuilder.Append($" WHERE");
				TranslatorDelegate.SetVisited(SectionType.Where);
			}
			Visit(expr.Body);
            return SqlBuilder.ToString();
        }

        private string GetParameterName()
        {
            _paramIndex++;
            return $"@param_{_paramIndex}";
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
                    SqlBuilder.Append(" != ");
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
            //return base.VisitBinary(node);
        }


        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "In" || node.Method.Name == "NotIn")
            {
                if (node.Arguments[0] is UnaryExpression)
                {
                    var unaryExpr = node.Arguments[0] as UnaryExpression;
                    var memberExpr = unaryExpr.Operand as MemberExpression;
                    if (memberExpr != null)
                    {
                        if (TranslatorDelegate.IsMultipleType())
                        {
                            SqlBuilder.Append($" `{TranslatorDelegate.GetTypeAlias(memberExpr.Member.DeclaringType)}`.");
                        }
                        SqlBuilder.Append($"`{TranslatorDelegate.GetMemberMap(memberExpr.Member)}`");
                    }
                }
                SqlBuilder.Append($" {node.Method.Name.ToUpper()}");
                if (node.Arguments[1] is NewArrayExpression)
                {
                    var newArrayExpr = node.Arguments[1] as NewArrayExpression;
                    SqlBuilder.Append($" (");
                    foreach (var expr in newArrayExpr.Expressions)
                    {
                        SqlBuilder.Append($"{((ConstantExpression)expr).Value},");
                    }
                    SqlBuilder.Remove(SqlBuilder.Length - 1, 1);
                    SqlBuilder.Append($")");
                }
                return node;
            }
            if (node.Method.Name == "Like" || node.Method.Name == "LeftLike" || node.Method.Name == "RightLike")
            {
                if (node.Arguments[0] is MemberExpression)
                {
                    var memberExpr = node.Arguments[0] as MemberExpression;
					if (TranslatorDelegate.IsMultipleType())
					{
                        SqlBuilder.Append($" `{TranslatorDelegate.GetTypeAlias(memberExpr.Member.DeclaringType)}`.");
                    }
                    SqlBuilder.Append($"`{TranslatorDelegate.GetMemberMap(memberExpr.Member)}`");
                    var paramName = GetParameterName();
                    SqlBuilder.Append($" LIKE {paramName}");
                    if (node.Arguments[1] is ConstantExpression)
                    {
                        var constantExpr = node.Arguments[1] as ConstantExpression;
                        switch (node.Method.Name)
                        {
                            case "Like":
                                _parameters.Add($"{paramName}", $"'%{constantExpr.Value}%'");
                                break;
                            case "LeftLike":
                                _parameters.Add($"{paramName}", $"'{constantExpr.Value}%'");
                                break;
                            case "RightLike":
                                _parameters.Add($"{paramName}", $"'%{constantExpr.Value}'");
                                break;
                        }
                    }
                }

                return node;
            }



            return base.VisitMethodCall(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
			if (TranslatorDelegate.IsMultipleType())
			{
                SqlBuilder.Append($" `{TranslatorDelegate.GetTypeAlias(node.Member.DeclaringType)}`.");
            }
            SqlBuilder.Append($"`{TranslatorDelegate.GetMemberMap(node.Member)}`");
            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            var paramName = GetParameterName();
            SqlBuilder.Append(paramName);
            _parameters.Add(paramName, node.Value);
            return node;
            //return base.VisitConstant(node);
        }
    }
}
