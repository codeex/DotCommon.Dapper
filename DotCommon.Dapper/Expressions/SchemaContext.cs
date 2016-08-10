using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions
{
    public class SchemaContext
    {
        public IDbConnection Connection { get; }

        public SchemaContext(IDbConnection connection)
        {
            Connection = connection;
        }

        public SchemaContext<T1> From<T1>()
            where T1 : class
        {
            return new SchemaContext<T1>(new BaseSchemaContext(Connection));
        }

    }

    public class BaseSchemaContext
    {
        public IDbConnection Connection { get; }
	    public QueryWapper Wapper { get; } 

		public BaseSchemaContext()
        {
        }

	    public BaseSchemaContext(IDbConnection connection)
	    {
		    Connection = connection;
			Wapper = new QueryWapper();
	    }

	    public BaseSchemaContext(IDbConnection connection, QueryWapper wapper)
	    {
		    Connection = connection;
		    Wapper = wapper;
	    }



    }

    public class SchemaContext<T1>:BaseSchemaContext
        where T1 : class
    {
        public SchemaContext(BaseSchemaContext baseContext) : base(baseContext.Connection, baseContext.Wapper)
        {

        }

        public SchemaContext<T1, T2> InnerJoin<T2>(Expression<Func<T1, T2, bool>> expression)
            where T2 : class
        {
            Wapper.AttachSectionItem(SectionType.Join, new SectionItem(expression));
            return new SchemaContext<T1, T2>(this);
        }

        public SchemaContext<T1> Union<TUnion>(Expression<Func<TUnion, bool>> expression)
        {
            Wapper.AttachSectionItem(SectionType.Union, new SectionItem(expression));
            return this;
        }

        public SchemaContext<T1> Where(Expression<Func<T1, bool>> expression)
        {
            Wapper.AttachSectionItem(SectionType.Where, new SectionItem(expression));
            return this;
        }

        public SchemaContext<T1> GroupBy(Expression<Func<T1, object>> expression)
        {
            Wapper.AttachSectionItem(SectionType.GroupBy, new SectionItem(expression));
            return this;
        }

        public SchemaContext<T1> Having(Expression<Func<T1, bool>> expression)
        {
            Wapper.AttachSectionItem(SectionType.Having, new SectionItem(expression));
            return this;
        }

        public SchemaContext<T1> OrderBy<TKey>(Expression<Func<T1, TKey>> expression, bool isAsc = true)
        {
            Wapper.AttachSectionItem(SectionType.OrderBy, new SectionItem(expression));
            return this;
        }

        public SchemaContext<T1> Top(int top)
	    {
            Wapper.AttachSectionItem(SectionType.Top, new SectionItem(sectionParameter: new TopSectionParameter(top)));
            return this;
	    }

        public SchemaContext<T1> Page(int pageIndex,int pageCount)
        {
            Wapper.AttachSectionItem(SectionType.Page,
                new SectionItem(sectionParameter: new PageSectionParameter(pageIndex,pageCount)));
            return this;
        }

        public IEnumerable<T1> Select()
        {
            using (Connection)
            {
                return Connection.Query<T1>("", new {});
            }
        }

    }

    public class SchemaContext<T1, T2> :BaseSchemaContext
        where T1 : class
        where T2 : class
    {
        public SchemaContext(BaseSchemaContext baseContext) : base(baseContext.Connection, baseContext.Wapper)
        {

        }

        public SchemaContext<T1, T2, T3> InnerJoin<T3>(Expression<Func<T1, T2, T3, bool>> expression)
            where T3 : class
        {
			Wapper.AttachSectionItem(SectionType.Join, new SectionItem(expression));
            return new SchemaContext<T1, T2, T3>(this);
        }



	    public SchemaContext<T1, T2> Where(Expression<Func<T1, T2, bool>> expression)
	    {
            Wapper.AttachSectionItem(SectionType.Where, new SectionItem(expression));
            return this;
	    }

	    public SchemaContext<T1, T2> GroupBy(Expression<Func<T1, T2, object>> expression)
	    {
            Wapper.AttachSectionItem(SectionType.GroupBy, new SectionItem(expression));
            return this;
	    }

	    public SchemaContext<T1, T2> Having(Expression<Func<T1, T2, bool>> expression)
	    {
			Wapper.AttachSectionItem(SectionType.Having, new SectionItem(expression));
		    return this;
	    }


        public SchemaContext<T1, T2> OrderBy<TKey>(Expression<Func<T1, T2, TKey>> expression, bool isAsc = true)
        {
            Wapper.AttachSectionItem(SectionType.OrderBy, new SectionItem(expression));
            return this;
        }

        public SchemaContext<T1, T2> Top(int top)
        {
            Wapper.AttachSectionItem(SectionType.OrderBy,
                new SectionItem(sectionParameter: new TopSectionParameter(top)));
            return this;
        }

        public SchemaContext<T1, T2> Page(int pageIndex, int pageCount)
        {
            Wapper.AttachSectionItem(SectionType.Page,
                new SectionItem(sectionParameter: new PageSectionParameter(pageIndex, pageCount)));
            return this;
        }

        public IEnumerable<T> Select<T>(Expression<Func<T1, T2, T>> expression)
        {
            return Connection.Query<T>("", new { });
        }
    }

    public class SchemaContext<T1, T2, T3>:BaseSchemaContext
        where T1 : class
        where T2 : class
        where T3 : class
    {
        public SchemaContext(BaseSchemaContext baseContext) : base(baseContext.Connection, baseContext.Wapper)
        {

        }


        public SchemaContext<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> expression)
        {
			Wapper.AttachSectionItem(SectionType.Where ,new SectionItem(expression));
			return this;
        }

        public SchemaContext<T1, T2, T3> GroupBy(Expression<Func<T1, T2, T3, object>> expression)
        {
            Wapper.AttachSectionItem(SectionType.GroupBy, new SectionItem(expression));
            return this;
        }

	    public SchemaContext<T1, T2, T3> Having(Expression<Func<T1, T2, T3, bool>> expression)
	    {
            Wapper.AttachSectionItem(SectionType.Having, new SectionItem(expression));
            return this;
	    }


	    public SchemaContext<T1, T2, T3> OrderBy<TKey>(Expression<Func<T1, T2, T3, TKey>> expression, bool isAsc = true)
	    {
            Wapper.AttachSectionItem(SectionType.OrderBy, new SectionItem(expression));
            return this;
	    }

        public SchemaContext<T1, T2, T3> Top(int top)
        {
            Wapper.AttachSectionItem(SectionType.Top, new SectionItem(sectionParameter: new TopSectionParameter(top)));
            return this;
        }

        public SchemaContext<T1, T2, T3> Page(int pageIndex, int pageCount)
        {
            Wapper.AttachSectionItem(SectionType.Page,
                new SectionItem(sectionParameter: new PageSectionParameter(pageIndex, pageCount)));
            return this;
        }

        public IEnumerable<T> Select<T>(Expression<Func<T1, T2, T3, T>> expression) 
            where T : class, new()
        {
            return new List<T>();
        }

        public IEnumerable<T> Select<T>()
            where T:T1,T2,T3
        {
            return new List<T>();
        }
    }


}
