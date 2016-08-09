using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotCommon.Dapper.FluentMap
{
    public class FluentMapConfiguration
    {
        private static readonly ConcurrentDictionary<Type, IEntityMap> EntityMaps =
            new ConcurrentDictionary<Type, IEntityMap>();

        public static void Initialize(List<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                //查询程序集中所有继承自IEntityMap<> 接口的实体
                var mapTypes = types.Where(x => x.GetInterface(typeof(IEntityMap<>).FullName) != null);
                foreach (var mapType in mapTypes)
                {
                    IEntityMap eMap;
                    if (!EntityMaps.TryGetValue(mapType, out eMap))
                    {
                        var entityMap = Activator.CreateInstance(mapType) as IEntityMap;
                        if (entityMap != null)
                        {
                            EntityMaps.TryAdd(entityMap.GetEntityType(), entityMap);
                        }
                    }
                }
            }
        }

        public static void Register(Func<IEntityMap> func)
        {
            var entityMap = func.Invoke();
            EntityMaps.TryAdd(entityMap.GetEntityType(), entityMap);
        }


        public static IEntityMap GetMap<TEntity>()
            where TEntity : class
        {
            return GetMap(typeof(TEntity));
        }


        public static IEntityMap GetMap(Type entityType)
        {
            IEntityMap entityMap;
            EntityMaps.TryGetValue(entityType, out entityMap);
            return entityMap;
        }
    }
}
