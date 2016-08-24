using System;
using System.Collections.Generic;
using System.Linq;
using DotCommon.Dapper.Expressions.Sections;
using DotCommon.Dapper.FluentMap;

namespace DotCommon.Dapper.Expressions.Builder
{
    public abstract class BaseQueryBuilder : IQueryBuilder
    {
        public SqlType SqlType { get; protected set; }

        public QueryWapper QueryWapper { get; protected set; }

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

    }
}
