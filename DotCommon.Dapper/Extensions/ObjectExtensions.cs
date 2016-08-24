using System.Reflection;
using DotCommon.Dapper.Expressions;

namespace DotCommon.Dapper.Extensions
{
    public static class ObjectExtensions
    {
        public static PropInfo ToProp(this MemberInfo member)
        {
            return new PropInfo(member);
        }

        public static bool In<T>(this object obj, params T[] parameters)
        {
            return true;
        }

        public static bool NotIn<T>(this object obj, params T[] parameters)
        {
            return true;
        }

        public static bool Like<T>(this object obj, T t)
        {
            return true;
        }

        public static bool RightLike<T>(this object obj, T t)
        {
            return true;
        }

        public static bool LeftLike<T>(this object obj, T t)
        {
            return true;
        }

	    public static T SqlAvg<T>(this T t)
	    {
		    return default(T);
	    }

        public static T SqlSum<T>(this T t)
        {
            return default(T);
        }

        public static int SqlCount(this object obj)
        {
            return 0;
        }

    }
}
