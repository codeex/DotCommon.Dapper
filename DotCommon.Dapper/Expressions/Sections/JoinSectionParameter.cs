namespace DotCommon.Dapper.Expressions.Sections
{
    public class JoinSectionParameter : ISectionParameter
    {
        public JoinType JoinType { get; private set; }

        public JoinSectionParameter(JoinType joinType)
        {
            JoinType = joinType;
        }
    }
}
