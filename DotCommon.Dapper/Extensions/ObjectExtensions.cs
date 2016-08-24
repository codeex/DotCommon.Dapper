using System.Reflection;
using DotCommon.Dapper.Expressions;
using DotCommon.Dapper.Expressions.Translators;

namespace DotCommon.Dapper.Extensions
{
    public static class ObjectExtensions
    {
        public static PropInfo ToProp(this MemberInfo member)
        {
            return new PropInfo(member);
        }

        public static bool In<TKey>(this object obj, params TKey[] parameters)
        {
            return true;
        }

        public static bool NotIn<TKey>(this object obj, params TKey[] parameters)
        {
            return true;
        }

        public static bool Like<TKey>(this object obj, TKey tkey)
        {
            return true;
        }

        public static bool RightLike<TKey>(this object obj, TKey tkey)
        {
            return true;
        }

        public static bool LeftLike<TKey>(this object obj, TKey tkey)
        {
            return true;
        }

	    public static TKey SqlAvg<TKey>(this TKey obj)
	    {
		    return default(TKey);
	    }

        public static TKey SqlSum<TKey>(this TKey obj)
        {
            return default(TKey);
        }

        public static int SqlCount(this object obj)
        {
            return 0;
        }

    }
}
