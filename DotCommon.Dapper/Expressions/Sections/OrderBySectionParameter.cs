namespace DotCommon.Dapper.Expressions.Sections
{
    public struct OrderBySectionParameter : ISectionParameter
    {
        public bool IsAsc { get; private set; }

        public OrderBySectionParameter(bool isAsc)
        {
            IsAsc = isAsc;
        }
    }
}
