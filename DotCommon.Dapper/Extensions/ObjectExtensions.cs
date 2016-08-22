namespace DotCommon.Dapper.Extensions
{
    public static class ObjectExtensions
    {
        public static bool In<TKey>(this object obj, params TKey[] parameters)
        {
            return true;
        }

        public static bool NotIn<TKey>(this object obj, params TKey[] parameters)
        {
            return false;
        }
    }
}
