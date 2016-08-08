using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotCommon.Dapper.Common;

namespace DotCommon.Dapper.Expressions
{
	public class QueryWapper
	{
		public List<BaseSection> Sections = new List<BaseSection>();

		public QueryWapper()
		{

		}

		public QueryWapper(List<BaseSection> sections)
		{
			if (sections != null)
			{
				Sections = sections;
			}
		}

		public void AttachSectionItem<TSection>(SectionItem item) where TSection : BaseSection, new()
		{
			var section = Sections.FirstOrDefault(x => x.GetType() == typeof (TSection)) ?? new TSection();
			section.AttachItem(item);
		}

		private BaseSection GetSection<TSection>(bool notNull = false) where TSection : BaseSection
		{
			var section = Sections.FirstOrDefault(x => x.GetType() == typeof (TSection));
			Ensure.NotNull(section, typeof (TSection).Name);
			return section;
		}

		public IEnumerable<Expression> GetExpressions<TSection>(bool notNull = false) where TSection : BaseSection
		{
			var section = GetSection<TSection>(notNull);
			return section?.Items.Select(x => x.Expression);
		}

		public Expression GetExpression<TSection>(bool notNull = false) where TSection : BaseSection
		{
			var section = GetSection<TSection>(notNull);
			return section?.Items.SingleOrDefault()?.Expression;
		}

		public TParameter GetSectionParameter<TSection, TParameter>(bool notNull = false)
			where TSection : BaseSection
			where TParameter : ISectionParameter

		{
			var section = GetSection<TSection>(notNull);
			return (TParameter) section?.Items.SingleOrDefault()?.SectionParameter;
		}


	}
}
