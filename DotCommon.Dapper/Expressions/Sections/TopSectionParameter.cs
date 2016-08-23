namespace DotCommon.Dapper.Expressions.Sections
{
    public struct TopSectionParameter : ISectionParameter
    {
        public int Top { get; private set; }

        public TopSectionParameter(int top)
        {
            Top = top;
        }
    }
}
