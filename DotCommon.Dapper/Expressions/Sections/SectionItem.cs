using System.Linq.Expressions;

namespace DotCommon.Dapper.Expressions.Sections
{
    public class SectionItem
    {
        public Expression Expression { get; private set; }

        public ISectionParameter SectionParameter { get; private set; }

        public SectionItem(Expression expression = null, ISectionParameter sectionParameter = null)
        {
            Expression = expression;
            SectionParameter = sectionParameter;
        }
    }
}
