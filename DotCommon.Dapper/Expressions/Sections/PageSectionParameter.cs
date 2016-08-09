namespace DotCommon.Dapper.Expressions.Sections
{
    public class PageSectionParameter : ISectionParameter
    {
        public int PageCount { get; private set; }

        public int PageIndex { get; private set; }

        public PageSectionParameter(int pageIndex, int pageCount)
        {
            PageCount = pageCount;
            PageIndex = pageIndex;
        }
    }
}
