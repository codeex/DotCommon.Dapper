namespace DotCommon.Dapper.Expressions.Sections
{
    public struct PageSectionParameter : ISectionParameter
    {
        public int PageSize { get; private set; }

        public int PageIndex { get; private set; }

        public PageSectionParameter(int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }
    }
}
