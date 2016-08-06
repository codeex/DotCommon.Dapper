﻿using System.Collections.Generic;

namespace DotCommon.Dapper.Expressions.Sections
{
	public abstract class BaseSection
	{
	    public List<SectionItem> Items { get; } = new List<SectionItem>();

		protected BaseSection()
		{
			
		}

		public void AddItem(SectionItem item)
		{
			Items.Add(item);
		}
	}
}
