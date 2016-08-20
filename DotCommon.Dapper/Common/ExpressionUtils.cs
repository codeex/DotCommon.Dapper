using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotCommon.Dapper.Common
{
	public class ExpressionUtils
	{
	    /// <summary>获取全部参数类型
	    /// </summary>
	    public static List<Type> GetParameterTypes(LambdaExpression expr)
	    {
	        return expr?.Parameters.Select(parameter => parameter.Type).ToList() ?? new List<Type>();
	    }
	}
}
