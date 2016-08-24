using System;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
	public class TranslatorDelegate
	{
		public Func<Type, string> GetTableName { get; private set; }

		public Func<PropInfo, string> GetPropMap { get; private set; }

		public Func<Type, string> GetTypeAlias { get; private set; }

		/// <summary>表示该部分是否由多个lambda表达式组合而成
		/// </summary>
		public Func<SectionType, bool> SectionIsMultiple { get; private set; }

		///////////////////////////////////////////////////

		/// <summary>是否为多表
		/// </summary>
		public Func<bool> IsMultipleType { get; private set; }

		/// <summary>设置为多表
		/// </summary>
		public Action SetMultipleType { get; private set; }

		/// <summary>是否为第一个访问
		/// </summary>
		public Func<SectionType, bool> IsFirstVisit { get; private set; }

		/// <summary>设置访问过
		/// </summary>
		public Action<SectionType> SetVisited { get; private set; }

		public TranslatorDelegate()
		{

		}

		public TranslatorDelegate(Func<Type, string> getTableName, Func<PropInfo, string> getPropMap)
		{
			GetTableName = getTableName;
			GetPropMap = getPropMap;
		}

		public TranslatorDelegate(Func<Type, string> getTableName, Func<PropInfo, string> getPropMap,
			Func<Type, string> getTypeAlias, Func<SectionType, bool> sectionIsMultiple)
		{
			GetTableName = getTableName;
			GetPropMap = getPropMap;
			GetTypeAlias = getTypeAlias;
			SectionIsMultiple = sectionIsMultiple;
		}

		public TranslatorDelegate(Func<Type, string> getTableName, Func<PropInfo, string> getPropMap,
			Func<Type, string> getTypeAlias, Func<SectionType, bool> sectionIsMultiple, Func<bool> isMultipleType,
			Action setMultipleType, Func<SectionType, bool> isFirstVisit, Action<SectionType> setVisited)
		{
			GetTableName = getTableName;
			GetPropMap = getPropMap;
			GetTypeAlias = getTypeAlias;
			SectionIsMultiple = sectionIsMultiple;
			IsMultipleType = isMultipleType;
			SetMultipleType = setMultipleType;
			IsFirstVisit = isFirstVisit;
			SetVisited = setVisited;
		}
	}


}
