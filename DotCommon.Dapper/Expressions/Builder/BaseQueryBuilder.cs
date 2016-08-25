using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotCommon.Dapper.Common;
using DotCommon.Dapper.Expressions.Sections;
using DotCommon.Dapper.Expressions.Translators;
using DotCommon.Dapper.FluentMap;

namespace DotCommon.Dapper.Expressions.Builder
{
    public abstract class BaseQueryBuilder : IQueryBuilder
    {
        public SqlType SqlType { get; protected set; }
        public QueryWapper QueryWapper { get; protected set; }
        protected StringBuilder SqlBuilder { get; set; }

        protected TranslatorDelegate TranslatorDelegate;

        protected bool IsMultipleType { get; set; } = false;

        private readonly Dictionary<SectionType, bool> _sectionVisitDict = new Dictionary<SectionType, bool>
        {
            {SectionType.Select, true},
            {SectionType.Where, true},
            {SectionType.GroupBy, true},
            {SectionType.Having, true},
            {SectionType.Join, true},
            {SectionType.OrderBy, true},
            {SectionType.Page, true},
            {SectionType.Top, true},
            {SectionType.Union, true}
        };


        protected Dictionary<Type, string> TypeAliasDict = new Dictionary<Type, string>();


        protected BaseQueryBuilder(SqlType sqlType, QueryWapper queryWapper)
        {
            SqlType = sqlType;
            QueryWapper = queryWapper;
        }

        protected Section FindSection(SectionType sectionType)
        {
            return QueryWapper.FindSection(sectionType);
        }


        /// <summary>获取属性映射的数据库字段的名称
        /// </summary>
        protected string GetMapName(PropInfo propInfo)
        {
            var entityMap = FluentMapConfiguration.GetMap(propInfo.Type);
            var propertyMap = entityMap?.PropertyMaps.FirstOrDefault(x => x.PropertyInfo.Name == propInfo.PropName);
            return propertyMap == null ? propInfo.PropName : propertyMap.ColumnName;
        }

        /// <summary>根据类型获取表名称
        /// </summary>
        protected string GetTableName(Type type)
        {
            var entityMap = FluentMapConfiguration.GetMap(type);
            return entityMap == null ? type.Name : entityMap.TableName;
        }

        /// <summary>获取类型对应表名-->表别名
        /// </summary>
        protected string GetTypeAlias(Type type)
        {
            var alias = "";
            TypeAliasDict.TryGetValue(type, out alias);
            return alias;
        }

        /// <summary>设置类型对应表名-->表别名 映射
        /// </summary>
        protected void SetAliasDict(Dictionary<Type, string> aliasDict)
        {
            foreach (var kv in aliasDict)
            {
                TypeAliasDict.Add(kv.Key, kv.Value);
            }
        }

        /// <summary>判断是否由多个部分组成
        /// </summary>
        protected bool SectionIsMultiple(SectionType sectionType)
        {
            return true;
        }

        protected bool GetIsMultipleType()
        {
            return IsMultipleType;
        }

        protected void SetMultipleType()
        {
            IsMultipleType = true;
        }

        protected bool IsFirstVisit(SectionType sectionType)
        {
            return _sectionVisitDict[sectionType];
        }

        protected void SetVisited(SectionType sectionType)
        {
            _sectionVisitDict[sectionType] = true;
        }


        protected SqlServerQueryTranslator CreateTranslator(SectionType sectionType, ISectionParameter parameter)
        {
            return default(SqlServerQueryTranslator);
        }


    }


}
