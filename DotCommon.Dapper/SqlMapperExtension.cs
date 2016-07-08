using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DotCommon.Dapper.Common;

namespace DotCommon.Dapper
{
    public static class SqlMapperExtensions
    {
        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> ParamCache =
            new ConcurrentDictionary<Type, List<PropertyInfo>>();

        private static readonly Dictionary<string, IConciseSqlBuilder> SqlBuilderDict = new Dictionary
            <string, IConciseSqlBuilder>()
        {
            {"sqlconnection", new SqlServerConciseBuilder()},
            {"mysqlconnection", new MySqlConciseBuilder()}
        };
        private static string GetConnectionName(IDbConnection connection)
        {
            return connection.GetType().Name.ToLower();
        }

        public static IConciseSqlBuilder GetConciseSqlBuilder(this IDbConnection connection)
        {
            var name = GetConnectionName(connection);
            return SqlBuilderDict.ContainsKey(name) ? SqlBuilderDict[name] : new SqlServerConciseBuilder();
        }

        /// <summary>插入数据库,返回插入的主键Id
        /// </summary>
        public static long Insert(this IDbConnection connection, dynamic data, string table,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var properties = GetProperties(obj);
            var sqlBuilder = connection.GetConciseSqlBuilder();
            var sql = sqlBuilder.BuildInsert(table, properties);
            return connection.ExecuteScalar<long>(sql, obj, transaction, commandTimeout);
        }

        /// <summary>插入数据库,返回影响的行数
        /// </summary>
        public static int InsertEffect(this IDbConnection connection, dynamic data, string table,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var properties = GetProperties(obj);
            var sqlBuilder = connection.GetConciseSqlBuilder();
            var sql = sqlBuilder.BuildInsertEffact(table, properties);
            return connection.Execute(sql, obj, transaction, commandTimeout);
        }

        /// <summary>异步插入数据库,返回插入的主键Id
        /// </summary>
        public static Task<long> InsertAsync(this IDbConnection connection, dynamic data, string table,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var properties = GetProperties(obj);
            var sqlBuilder = connection.GetConciseSqlBuilder();
            var sql = sqlBuilder.BuildInsert(table, properties);
            return connection.ExecuteScalarAsync<long>(sql, obj, transaction, commandTimeout);
        }

        /// <summary>异步插入数据库,返回影响的行数
        /// </summary>
        public static Task<int> InsertEffectAsync(this IDbConnection connection, dynamic data, string table,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var properties = GetProperties(obj);
            var sqlBuilder = connection.GetConciseSqlBuilder();
            var sql = sqlBuilder.BuildInsertEffact(table, properties);
            return connection.ExecuteAsync(sql, obj, transaction, commandTimeout);
        }

        /// <summary>更新数据
        /// </summary>
        public static int Update(this IDbConnection connection, dynamic data, dynamic condition, string table,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var conditionObj = condition as object;

            var updatePropertyInfos = GetPropertyInfos(obj);
            var wherePropertyInfos = GetPropertyInfos(conditionObj);

            var updateProperties = updatePropertyInfos.Select(p => p.Name).ToList();
            var whereProperties = wherePropertyInfos.Select(p => p.Name).ToList();

            var builder = connection.GetConciseSqlBuilder();
            var sql = builder.BuildUpdate(table, updateProperties, whereProperties);

            var parameters = new DynamicParameters(data);
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;
            wherePropertyInfos.ForEach(p => expandoObject.Add("w_" + p.Name, p.GetValue(conditionObj, null)));
            parameters.AddDynamicParams(expandoObject);

            return connection.Execute(sql, parameters, transaction, commandTimeout);
        }


        /// <summary>异步更新数据
        /// </summary>
        public static Task<int> UpdateAsync(this IDbConnection connection, dynamic data, dynamic condition, string table,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var conditionObj = condition as object;

            var updatePropertyInfos = GetPropertyInfos(obj);
            var wherePropertyInfos = GetPropertyInfos(conditionObj);

            var updateProperties = updatePropertyInfos.Select(p => p.Name).ToList();
            var whereProperties = wherePropertyInfos.Select(p => p.Name).ToList();

            var builder = connection.GetConciseSqlBuilder();
            var sql = builder.BuildUpdate(table, updateProperties, whereProperties);

            var parameters = new DynamicParameters(data);
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;
            wherePropertyInfos.ForEach(p => expandoObject.Add("w_" + p.Name, p.GetValue(conditionObj, null)));
            parameters.AddDynamicParams(expandoObject);
            return connection.ExecuteAsync(sql, parameters, transaction, commandTimeout);
        }

        /// <summary>删除数据
        /// </summary>
        public static int Delete(this IDbConnection connection, dynamic condition, string table,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var builder = connection.GetConciseSqlBuilder();
            var whereProperties = GetProperties(conditionObj);
            var sql = builder.BuildDelete(table, whereProperties);
            return connection.Execute(sql, conditionObj, transaction, commandTimeout);
        }

        /// <summary>异步删除数据
        /// </summary>
        public static Task<int> DeleteAsync(this IDbConnection connection, dynamic condition, string table,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var builder = connection.GetConciseSqlBuilder();
            var whereProperties = GetProperties(conditionObj);
            var sql = builder.BuildDelete(table, whereProperties);
            return connection.ExecuteAsync(sql, conditionObj, transaction, commandTimeout);
        }

        /// <summary>查询数量
        /// </summary>
        public static int GetCount(this IDbConnection connection, object condition, string table, bool isOr = false,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var properties = GetProperties(condition);
            var builder = connection.GetConciseSqlBuilder();
            var sql = builder.BuildCount(table, properties, isOr);
            return connection.ExecuteScalar<int>(sql, condition, transaction, commandTimeout);
        }

        /// <summary>异步查询数量
        /// </summary>
        public static Task<int> GetCountAsync(this IDbConnection connection, object condition, string table,
            bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var properties = GetProperties(condition);
            var builder = connection.GetConciseSqlBuilder();
            var sql = builder.BuildCount(table, properties, isOr);
            return connection.ExecuteScalarAsync<int>(sql, condition, transaction, commandTimeout);
        }

        /// <summary>Query a list of data from table with a specified condition.
        /// </summary>
        public static IEnumerable<dynamic> QueryList(this IDbConnection connection, dynamic condition, string table,
            string columns = "*", bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryList<dynamic>(connection, condition, table, columns, isOr, transaction, commandTimeout);
        }

        /// <summary>查询集合数据
        /// </summary>
        public static Task<IEnumerable<dynamic>> QueryListAsync(this IDbConnection connection, dynamic condition,
            string table, string columns = "*", bool isOr = false, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            return QueryListAsync<dynamic>(connection, condition, table, columns, isOr, transaction, commandTimeout);
        }

        /// <summary>查询集合数据
        /// </summary>
        public static IEnumerable<T> QueryList<T>(this IDbConnection connection, object condition, string table,
            string columns = "*", bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var properties = GetProperties(condition);
            var builder = connection.GetConciseSqlBuilder();
            var sql = builder.BuildQuerySql(table, properties, columns, isOr);
            return connection.Query<T>(sql, condition, transaction, true, commandTimeout);
        }

        /// <summary>Query a list of data async from table with specified condition.
        /// </summary>
        public static Task<IEnumerable<T>> QueryListAsync<T>(this IDbConnection connection, object condition,
            string table, string columns = "*", bool isOr = false, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            var properties = GetProperties(connection);
            var builder = connection.GetConciseSqlBuilder();
            var sql = builder.BuildQuerySql(table, properties, columns, isOr);
            return connection.QueryAsync<T>(sql, condition, transaction, commandTimeout);
        }

        /// <summary>分页查询
        /// </summary>
        public static IEnumerable<dynamic> QueryPaged(this IDbConnection connection, dynamic condition, string table,
            string orderBy, int pageIndex, int pageSize, string columns = "*", bool isOr = false,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryPaged<dynamic>(connection, condition, table, orderBy, pageIndex, pageSize, columns, isOr,
                transaction, commandTimeout);
        }


        /// <summary>异步分页查询
        /// </summary>
        public static Task<IEnumerable<dynamic>> QueryPagedAsync(this IDbConnection connection, dynamic condition,
            string table, string orderBy, int pageIndex, int pageSize, string columns = "*", bool isOr = false,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryPagedAsync<dynamic>(connection, condition, table, orderBy, pageIndex, pageSize, columns, isOr,
                transaction, commandTimeout);
        }


        /// <summary>分页查询
        /// </summary>
        public static IEnumerable<T> QueryPaged<T>(this IDbConnection connection, dynamic condition, string table,
            string orderBy, int pageIndex, int pageSize, string columns = "*", bool isOr = false,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var properties = GetProperties(conditionObj);
            var builder = connection.GetConciseSqlBuilder();
            var sql = builder.BuildQueryPaged(table, properties, orderBy, pageIndex, pageSize, columns, isOr);
            return connection.Query<T>(sql, conditionObj, transaction, true, commandTimeout);
        }

        /// <summary>异步分页查询
        /// </summary>
        public static Task<IEnumerable<T>> QueryPagedAsync<T>(this IDbConnection connection, dynamic condition,
            string table, string orderBy, int pageIndex, int pageSize, string columns = "*", bool isOr = false,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var properties = GetProperties(conditionObj);
            var builder = connection.GetConciseSqlBuilder();
            var sql = builder.BuildQueryPaged(table, properties, orderBy, pageIndex, pageSize, columns, isOr);
            return connection.QueryAsync<T>(sql, conditionObj, transaction, commandTimeout);
        }

        private static List<string> GetProperties(object obj)
        {
            if (obj == null)
            {
                return new List<string>();
            }
            var parameters = obj as DynamicParameters;
            if (parameters != null)
            {
                return parameters.ParameterNames.ToList();
            }
            return GetPropertyInfos(obj).Select(x => x.Name).ToList();
        }

        private static List<PropertyInfo> GetPropertyInfos(object obj)
        {
            if (obj == null)
            {
                return new List<PropertyInfo>();
            }

            List<PropertyInfo> properties;
            if (ParamCache.TryGetValue(obj.GetType(), out properties))
            {
                return properties.ToList();
            }
            properties = ReflectionHelper.GetPropertyInfos(obj);
            ParamCache[obj.GetType()] = properties;
            return properties;
        }
    }
}
