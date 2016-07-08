using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Xunit;

namespace DotCommon.Dapper.Test
{
    public class SqlServerBuilderTest
    {
        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _paramCache =
            new ConcurrentDictionary<Type, List<PropertyInfo>>();
        private readonly IConciseSqlBuilder _sqlBuilder = new SqlServerConciseBuilder();
        private string _tableName = "Table1";

        private static readonly SqlBuilderTestObjectClass TestObject = new SqlBuilderTestObjectClass()
        {
            Id = 1,
            Mobile = "15868702111",
            Name = "cocosip"
        };
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
            if (_paramCache.TryGetValue(obj.GetType(), out properties)) return properties.ToList();
            properties =
                obj.GetType()
                    .GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public)
                    .ToList();
            _paramCache[obj.GetType()] = properties;
            return properties;
        }

        [Fact]
        public void BuildInsert_Test()
        {
            var properties = GetProperties(TestObject);
            string sql = _sqlBuilder.BuildInsert(_tableName, properties);
            string expectSql =
                "INSERT INTO Table1 (Id,Name,Mobile) VALUES (@Id,@Name,@Mobile) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
            Assert.Equal(expectSql.Trim(), sql.Trim());
        }

        [Fact]
        public void BuildInsertEffact_Test()
        {
            var properties = GetProperties(TestObject);
            string sql = _sqlBuilder.BuildInsertEffact(_tableName, properties).TrimEnd();
            string expectSql = "INSERT INTO Table1 (Id,Name,Mobile) VALUES (@Id,@Name,@Mobile)";
            Assert.Equal(expectSql.Trim(), sql.Trim());
        }

        [Fact]
        public void BuildUpdate_Test()
        {
            var updateProperties = GetProperties(new { Mobile = "123456" });
            var whereProperties = GetProperties(new { Id = 1 });
            string sql = _sqlBuilder.BuildUpdate(_tableName, updateProperties, whereProperties);
            string expectSql = "UPDATE Table1 SET Mobile = @Mobile WHERE Id = @w_Id";
            Assert.Equal(expectSql.Trim(), sql.Trim());
        }

        [Fact]
        public void BuildDelete_Test()
        {
            var whereProperties = GetProperties(new { Id = 1 });
            string sql = _sqlBuilder.BuildDelete(_tableName, whereProperties);
            string expectSql = "DELETE FROM Table1 WHERE Id = @Id";
            Assert.Equal(expectSql.Trim(), sql.Trim());
        }

        [Fact]
        public void BuildCount_Test()
        {
            var properties = GetProperties(new { Id = 1 });
            string sql = _sqlBuilder.BuildCount(_tableName, properties);
            string expectSql = "SELECT COUNT(*) FROM Table1 WHERE Id = @Id";
            Assert.Equal(expectSql.Trim(), sql.Trim());
        }

        [Fact]
        public void BuildQuerySql_Test()
        {
            var properties = GetProperties(new { Id = 1, Name = "cc" });
            string sql = _sqlBuilder.BuildQuerySql(_tableName, properties);
            string expectSql = "SELECT * FROM Table1 WHERE Id = @Id AND Name = @Name";
            Assert.Equal(expectSql.Trim(), sql.Trim());
        }

        [Fact]
        public void BuildQueryPaged_Test()
        {
            var properties = GetProperties(new { Id = 1, Name = "cc" });
            string sql = _sqlBuilder.BuildQueryPaged(_tableName, properties, "Id", 2, 5);
            string expectSql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id) AS RowNumber, * FROM Table1 WHERE Id = @Id AND Name = @Name) AS Total WHERE RowNumber >= 6 AND RowNumber <= 10";
            Assert.Equal(expectSql.Trim(), sql.Trim());
        }
    }
}
