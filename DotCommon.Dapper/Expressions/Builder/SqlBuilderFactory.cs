using System.Collections.Generic;
using DotCommon.Dapper.Common;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Builder
{
    public class SqlBuilderFactory
    {
        private static Stack<ISqlBuilder> _sqlServerBuilders;
        private static Stack<ISqlBuilder> _mySqlBuilders;
        static SqlBuilderFactory()
        {
            var setting = DapperConfiguration.Instance.Setting;
            _sqlServerBuilders = new Stack<ISqlBuilder>(setting.MaxSqlBuilder);
            _mySqlBuilders = new Stack<ISqlBuilder>(setting.MaxSqlBuilder);
        }

        //public ISqlBuilder CreateBuilder(SqlType sqlType, IWapper wapper)
        //{
        //    if (sqlType == SqlType.SqlServer)
        //    {
                
        //    }
        //}



    }

    public enum SqlType
    {
        SqlServer = 1,
        MySql = 2
    }
}
