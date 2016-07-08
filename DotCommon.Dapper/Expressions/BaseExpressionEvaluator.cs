using System.Collections.Generic;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions
{
    public abstract class BaseExpressionEvaluator : IExpressionEvaluator
    {
        protected List<BaseSection> Sections;

        protected void SetSections(List<BaseSection> sections)
        {
            Sections = sections;
        }

        
    }
}
