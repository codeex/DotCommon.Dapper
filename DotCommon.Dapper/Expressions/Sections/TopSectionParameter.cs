namespace DotCommon.Dapper.Expressions.Sections
{
    public class TopSectionParameter:ISectionParameter
    {
        public int Top { get; private set; }

        public TopSectionParameter(int top)
        {
            Top = top;
        }
    }
}
