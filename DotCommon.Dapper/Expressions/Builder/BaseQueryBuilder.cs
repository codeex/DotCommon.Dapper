using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotCommon.Dapper.Common;
using DotCommon.Dapper.Expressions.Sections;
using DotCommon.Dapper.FluentMap;

namespace DotCommon.Dapper.Expressions.Builder
{
    public abstract class BaseQueryBuilder : IQueryBuilder
    {
        public SqlType SqlType { get; protected set; }

        public QueryWapper QueryWapper { get; protected set; }

        /// <summary>类型别名关联
        /// </summary>
        private Dictionary<Type, string> _typeAliasDict;

        /// <summary>类型表明关联
        /// </summary>
        private Dictionary<Type, string> _typeTableDict;

 


        protected BaseQueryBuilder(SqlType sqlType, QueryWapper queryWapper)
        {
            SqlType = sqlType;
            QueryWapper = queryWapper;
        }

        protected Section FindSection(SectionType sectionType)
        {
            return QueryWapper.FindSection(sectionType);
        }

        /// <summary>获取Select中的全部类型
        /// </summary>
        protected List<Type> GetSelectTypes()
        {
            var expr = FindSection(SectionType.Select).Items.FirstOrDefault()?.Expression;
            return ExpressionUtils.GetParameterTypes((LambdaExpression) expr);
        }


        /// <summary>获取类型与别名集合
        /// </summary>
        private Dictionary<Type, string> GetTypeAliasDict()
        {
            if (_typeAliasDict != null && _typeAliasDict.Count > 0)
            {
                return _typeAliasDict;
            }
            _typeAliasDict = new Dictionary<Type, string>();
            var types = GetSelectTypes();
            int b = 97;
            foreach (var type in types)
            {
                _typeAliasDict.Add(type, ((char) b).ToString());
                b++;
            }
            return _typeAliasDict;
        }

        /// <summary>获取类型的别名
        /// </summary>
        protected string GetTypeAlias(Type type)
        {
            if (_typeAliasDict == null || _typeAliasDict.Count == 0)
            {
                GetTypeAliasDict();
            }
            return _typeAliasDict?.FirstOrDefault(x => x.Key == type).Value;
        }



        /// <summary>类型表名集合
        /// </summary>
        protected Dictionary<Type, string> GetTypeTable()
        {
            if (_typeTableDict != null && _typeTableDict.Count > 0)
            {
                return _typeTableDict;
            }
            _typeTableDict = new Dictionary<Type, string>();
            var types = GetSelectTypes();
            foreach (var type in types)
            {

            }

            return _typeTableDict;
        }


    }
}
