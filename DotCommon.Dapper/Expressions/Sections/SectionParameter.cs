namespace DotCommon.Dapper.Expressions.Sections
{
    public interface ISectionParameter
    {

    }

    public class TopSectionParameter : ISectionParameter
    {
        public int Top { get; private set; }

        public TopSectionParameter(int top)
        {
            Top = top;
        }
    }

    public class PageSectionParameter : ISectionParameter
    {
        public int PageCount { get; private set; }

        public int PageIndex { get; private set; }

        public PageSectionParameter(int pageCount, int pageIndex)
        {
            PageCount = pageCount;
            PageIndex = pageIndex;
        }
    }

}
