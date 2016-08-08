using System.Collections.Generic;

namespace DotCommon.Dapper.Expressions
{
	public abstract class BaseSection
	{
		public List<SectionItem> Items { get; } = new List<SectionItem>();

		public void AttachItem(SectionItem item)
		{
			Items.Add(item);
		}
	}

	public class GroupBySection : BaseSection
	{

	}

	public class HavingSection : BaseSection
	{

	}

	public class JoinSection : BaseSection
	{

	}

	public class OrderBySection : BaseSection
	{

	}
	public class TopSection : BaseSection
	{

	}
	public class PageSection : BaseSection
	{

	}
	public class UnionSection : BaseSection
	{

	}
	public class WhereSection : BaseSection
	{

	}
	public class SelectSection : BaseSection
	{

	}
}
