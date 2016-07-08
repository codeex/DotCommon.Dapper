using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotCommon.Dapper.Common;
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

        protected Expression GetSelectExpression()
        {
            var selectSection = Sections.FirstOrDefault(x => x.GetType() == typeof (SelectSection));
            Ensure.NotNull(selectSection, "SelectSection");
            return selectSection?.Expressions.FirstOrDefault();
        }

        protected Expression GetWhereExpression()
        {
            var whereSection = Sections.FirstOrDefault(x => x.GetType() == typeof (WhereSection));
            return whereSection?.Expressions.FirstOrDefault();
        }

        protected Expression GetOrderByExpression()
        {
            var orderBySection = Sections.FirstOrDefault(x => x.GetType() == typeof (OrderBySection));
            return orderBySection?.Expressions.FirstOrDefault();
        }

        protected PageSectionParameter GetPageSectionParameter()
        {
            var pageSection = Sections.FirstOrDefault(x => x.GetType() == typeof (PageSection));
            return pageSection?.SectionParameters.FirstOrDefault() as PageSectionParameter;
        }

        protected TopSectionParameter GetTopSectionParameter()
        {
            var topSection = Sections.FirstOrDefault(x => x.GetType() == typeof (TopSection));
            return topSection?.SectionParameters.FirstOrDefault() as TopSectionParameter;
        }

    }
}
