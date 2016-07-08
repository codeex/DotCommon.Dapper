using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotCommon.Dapper.FluentMap;
using Xunit;

namespace DotCommon.Dapper.Test.FluentMap
{
    public class FluentMapTest
    {
        [Fact]
        public void FluentMap_Test()
        {
            var map = new TestMapUser();
            Assert.Equal("T1", map.TableName);
            Assert.Equal(3, map.PropertyMaps.Count);
            var type = typeof(TestUser);
            Assert.Equal(type, map.GetEntityType());
            foreach (
                var propertyInfo in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
            {
                var propertyMap = map.PropertyMaps.FirstOrDefault(x => x.PropertyInfo == propertyInfo);
                if (propertyMap != null)
                {
                    if (propertyInfo.Name == "UserId")
                    {
                        Assert.Equal("uid", propertyMap.ColumnName);
                    }
                    if (propertyInfo.Name == "UserName")
                    {
                        Assert.Equal("tb_UserName", propertyMap.ColumnName);
                    }
                    if (propertyInfo.Name == "Age")
                    {
                        Assert.Equal(true, propertyMap.Ignored);
                    }
                }
            }
        }

        [Fact]
        public void FluentMapConfiguration_Test()
        {
            var assemblyList = new List<Assembly>()
            {
                Assembly.GetExecutingAssembly()
            };
            FluentMapConfiguration.Register(() => new TestMapUser());

            FluentMapConfiguration.Initialize(assemblyList);
            var entityMap = FluentMapConfiguration.GetMap(typeof(TestUser));
            var entityMap1 = FluentMapConfiguration.GetMap<TestUser>();
            Assert.Equal(entityMap, entityMap1);
            Assert.Equal("T1", entityMap.TableName);
            Assert.Equal(3, entityMap.PropertyMaps.Count);
            var type = typeof(TestUser);
            Assert.Equal(type, entityMap.GetEntityType());
            foreach (
                var propertyInfo in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public))
            {
                var propertyMap = entityMap.PropertyMaps.FirstOrDefault(x => x.PropertyInfo == propertyInfo);
                if (propertyMap != null)
                {
                    if (propertyInfo.Name == "UserId")
                    {
                        Assert.Equal("uid", propertyMap.ColumnName);
                    }
                    if (propertyInfo.Name == "UserName")
                    {
                        Assert.Equal("tb_UserName", propertyMap.ColumnName);
                    }
                    if (propertyInfo.Name == "Age")
                    {
                        Assert.Equal(true, propertyMap.Ignored);
                    }
                }
            }
        }

    }
}
