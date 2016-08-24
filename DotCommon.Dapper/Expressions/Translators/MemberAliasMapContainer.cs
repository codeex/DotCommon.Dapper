﻿using System.Collections.Generic;
using System.Reflection;
using DotCommon.Dapper.Extensions;

namespace DotCommon.Dapper.Expressions.Translators
{
	/// <summary>成员进行别名时的容器,如 Oid=x.OrderId,存放的是 Oid为Key,
	/// </summary>
	public class MemberAliasMapContainer
	{
		public List<MemberAliasItem> MemberAliasMaps { get; } = new List<MemberAliasItem>();
		private int _memberIndex = 0;

		public void SetAlias(string alias)
		{
			MemberAliasMaps.Add(new MemberAliasItem(alias));
		}

	    public void SetPropInfo(MemberInfo memberInfo)
	    {
	        var propInfo = memberInfo.ToProp();
	        SetPropInfo(propInfo);
	    }

	    public void SetPropInfo(PropInfo propInfo)
		{
			if (MemberAliasMaps.Count > _memberIndex)
			{
				MemberAliasMaps[_memberIndex].SetPropInfo(propInfo);
				_memberIndex++;
			}
		}

		//public override string ToString()
		//{
		//	StringBuilder sb = new StringBuilder();
		//	foreach (var map in MemberAliasMaps)
		//	{
		//		sb.Append($"[Alias:{map.Alias},Member:{map.Member.Name}]");
		//	}
		//	return sb.ToString();
		//}
	}

	public class MemberAliasItem
	{
		public MemberAliasItem(string alias)
		{
			Alias = alias;
		}

		public void SetPropInfo(PropInfo propInfo)
		{
            PropInfo = propInfo;
		}

		public string Alias { get; private set; }
		public PropInfo PropInfo { get; private set; }

        
	}
}
