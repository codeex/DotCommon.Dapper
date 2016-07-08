using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DotCommon.Dapper.Common;

namespace DotCommon.Dapper.FluentMap
{
    public interface IEntityMap
    {
        string TableName { get; }
        IList<IPropertyMap> PropertyMaps { get; }
        Type GetEntityType();
    }

    public interface IEntityMap<TEntity> : IEntityMap
        where TEntity : class
    {
    }

    public class EntityMap<TEntity> : IEntityMap<TEntity>
        where TEntity : class
    {
        public EntityMap()
        {
            PropertyMaps = new List<IPropertyMap>();
        }

        public string TableName { get; protected set; }
        public IList<IPropertyMap> PropertyMaps { get; }

        public Type GetEntityType()
        {
            return typeof (TEntity);
        }

        /// <summary>表名称
        /// </summary>
        public void Table(string tableName)
        {
            TableName = tableName;
        }

        /// <summary>具有的前缀名,对整个实体起作用,该优先级最低
        /// </summary>
        public void HasPrefix(string prefix)
        {
            var propertyInfoList = ReflectionHelper.GetPropertyInfos(typeof (TEntity));
            foreach (var propertyInfo in propertyInfoList)
            {
                //现有的属性映射中没有当前的属性
                if (PropertyMaps.FirstOrDefault(x => x.PropertyInfo == propertyInfo) == null)
                {
                    Map(propertyInfo).Column($"{prefix}{propertyInfo.Name}");
                }
            }
        }


        protected IPropertyMap Map(Expression<Func<TEntity, object>> expression)
        {
            PropertyInfo propertyInfo = ReflectionHelper.GetMemberInfo(expression) as PropertyInfo;
            return Map(propertyInfo);
        }

        public IPropertyMap Map(PropertyInfo propertyInfo)
        {
            var propertyMap = new PropertyMap(propertyInfo);
            var existMap = PropertyMaps.FirstOrDefault(x => x.PropertyInfo == propertyInfo);
            if (existMap != null)
            {
                PropertyMaps.Remove(existMap);
            }
            PropertyMaps.Add(propertyMap);
            return propertyMap;
        }
    }
}
