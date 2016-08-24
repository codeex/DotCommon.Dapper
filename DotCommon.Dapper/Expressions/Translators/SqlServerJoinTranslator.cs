using System;
using System.Linq.Expressions;
using DotCommon.Dapper.Expressions.Sections;
using DotCommon.Dapper.Extensions;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class SqlServerJoinTranslator: SqlServerQueryTranslator
    {
        public SqlServerJoinTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter)
            : base(translatorDelegate,parameter)
        {
        }

        public override string Translate(LambdaExpression expr)
        {
            Visit(expr.Body);
            return SqlBuilder.ToString();
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var parameter = (JoinSectionParameter) Parameter;
            var join = " INNER JOIN";
            if (parameter.JoinType == JoinType.LeftJoin)
            {
                @join = " LEFT JOIN";
            }
            else if (parameter.JoinType == JoinType.RightJoin)
            {
                @join = " RIGHT JOIN";
            }
            string left = "", right = "";
            if (node.Left is MemberExpression)
            {
                var memberExprL = node.Left as MemberExpression;
                var typeAlias = TranslatorDelegate.GetTypeAlias(memberExprL.Member.DeclaringType);
                SqlBuilder.Append(
                    $" [{TranslatorDelegate.GetTableName(memberExprL.Member.DeclaringType)}] {typeAlias}");
                left =
                    $" [{typeAlias}].[{TranslatorDelegate.GetPropMap(memberExprL.Member.ToProp())}]";
            }
            SqlBuilder.Append($"{join}");

            if (node.Right is MemberExpression)
            {
                var memberExprR = node.Right as MemberExpression;
                var typeAlias = TranslatorDelegate.GetTypeAlias(memberExprR.Member.DeclaringType);
                SqlBuilder.Append(
                    $" [{TranslatorDelegate.GetTableName(memberExprR.Member.DeclaringType)}] {typeAlias}");
                right = $" [{typeAlias}].[{TranslatorDelegate.GetPropMap(memberExprR.Member.ToProp())}]";
            }
            if (node.NodeType != ExpressionType.Equal)
            {
                throw new ArgumentException("Join param invalid!");
            }

            SqlBuilder.Append($" ON{left} ={right}");
            return node;
        }
    }
}
