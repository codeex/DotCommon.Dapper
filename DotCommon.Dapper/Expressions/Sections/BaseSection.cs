using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotCommon.Dapper.Expressions.Sections
{
    public abstract class BaseSection
    {
        public List<Expression> Expressions = new List<Expression>();

        public List<ISectionParameter> SectionParameters = new List<ISectionParameter>(); 

        protected BaseSection()
        {

        }

        public void AttachExpression(Expression expression)
        {
            Expressions.Add(expression);
        }

        public void AttachParam(ISectionParameter sectionParameter)
        {
            SectionParameters.Add(sectionParameter);
        }
    }
}
