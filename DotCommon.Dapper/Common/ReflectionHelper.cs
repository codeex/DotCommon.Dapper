using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DotCommon.Dapper.Common
{
    public class ReflectionHelper
    {
        public static List<PropertyInfo> GetPropertyInfos(object obj)
        {
            return GetPropertyInfos(obj.GetType());
        }


        public static List<PropertyInfo> GetPropertyInfos(Type type)
        {
            return type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
        }

        /// <summary>
        /// Returns the <see cref="T:System.Reflection.MemberInfo"/> for the specified lamba expression.
        /// </summary>
        /// <param name="lambda">A lamba expression containing a MemberExpression.</param>
        /// <returns>A MemberInfo object for the member in the specified lambda expression.</returns>
        public static MemberInfo GetMemberInfo(LambdaExpression lambda)
        {
            Expression expr = lambda;
            while (true)
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;

                    case ExpressionType.Convert:
                        expr = ((UnaryExpression)expr).Operand;
                        break;

                    case ExpressionType.MemberAccess:
                        var memberExpression = (MemberExpression)expr;
                        var baseMember = memberExpression.Member;

                        // Make sure we get the property from the derived type.
                        var paramType = lambda.Parameters[0].Type;
                        var memberInfo = paramType.GetMember(baseMember.Name)[0];
                        return memberInfo;
                    default:
                        return null;
                }
            }
        }
    }
}
