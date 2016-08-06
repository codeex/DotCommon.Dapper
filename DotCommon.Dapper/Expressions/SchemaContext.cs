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
        public List<BaseSection> Sections { get; }

        public BaseSchemaContext()
        {
        }

        public BaseSchemaContext(IDbConnection connection)
        {
            Connection = connection;
            Sections = new List<BaseSection>();
        }

        public BaseSchemaContext(IDbConnection connection, List<BaseSection> sections)
        {
            Connection = connection;
            Sections = sections;
        }

	    protected void AddItem<TSection>(SectionItem item)
		    where TSection : BaseSection, new()
	    {
		    var section = Sections.FirstOrDefault(x => x.GetType() == typeof (TSection)) ?? new TSection();
		    section.AddItem(item);
	    }
    }

    public class SchemaContext<T1>:BaseSchemaContext
        where T1 : class
    {
        public SchemaContext(BaseSchemaContext baseContext) : base(baseContext.Connection, baseContext.Sections)
        {

        }

	    public SchemaContext<T1, T2> InnerJoin<T2>(Expression<Func<T1, T2, bool>> expression)
		    where T2 : class
	    {
		    AddItem<JoinSection>(new SectionItem(expression));
		    return new SchemaContext<T1, T2>(this);
	    }

	    public SchemaContext<T1> Union<TUnion>(Expression<Func<TUnion, bool>> expression)
	    {
		    AddItem<UnionSection>(new SectionItem(expression));
		    return this;
	    }

	    public SchemaContext<T1> Where(Expression<Func<T1, bool>> expression)
        {
			AddItem<WhereSection>(new SectionItem(expression));
			return this;
        }

        public SchemaContext<T1> GroupBy(Expression<Func<T1, object>> expression)
        {
			AddItem<GroupBySection>(new SectionItem(expression));
			return this;
        }

        public SchemaContext<T1> Having(Expression<Func<T1, bool>> expression)
        {
			AddItem<HavingSection>(new SectionItem(expression));
			return this;
        }

        public SchemaContext<T1> OrderBy<TKey>(Expression<Func<T1, TKey>> expression, bool isAsc = true)
        {
			AddItem<OrderBySection>(new SectionItem(expression));
			return this;
        }

	    public SchemaContext<T1> Top(int top)
	    {
		    AddItem<TopSection>(new SectionItem(sectionParameter: new TopSectionParameter(top)));
		    return this;
	    }

	    public SchemaContext<T1> Page(int pageCount, int pageIndex)
	    {
		    AddItem<TopSection>(new SectionItem(sectionParameter: new PageSectionParameter(pageCount, pageIndex)));
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
        public SchemaContext(BaseSchemaContext baseContext) : base(baseContext.Connection, baseContext.Sections)
        {

        }

        public SchemaContext<T1, T2, T3> InnerJoin<T3>(Expression<Func<T1, T2, T3, bool>> expression)
            where T3 : class
        {
			AddItem<JoinSection>(new SectionItem(expression));
            return new SchemaContext<T1, T2, T3>(this);
        }



	    public SchemaContext<T1, T2> Where(Expression<Func<T1, T2, bool>> expression)
	    {
		    AddItem<WhereSection>(new SectionItem(expression));
		    return this;
	    }

	    public SchemaContext<T1, T2> GroupBy(Expression<Func<T1, T2, object>> expression)
	    {
		    AddItem<GroupBySection>(new SectionItem(expression));
		    return this;
	    }

	    public SchemaContext<T1, T2> Having(Expression<Func<T1, T2, bool>> expression)
	    {
		    AddItem<HavingSection>(new SectionItem(expression));
		    return this;
	    }


	    public SchemaContext<T1, T2> OrderBy<TKey>(Expression<Func<T1, T2, TKey>> expression, bool isAsc = true)
	    {
		    AddItem<OrderBySection>(new SectionItem(expression));
		    return this;
	    }

	    public SchemaContext<T1, T2> Top(int top)
	    {
		    AddItem<OrderBySection>(new SectionItem(sectionParameter: new TopSectionParameter(top)));
		    return this;
	    }

	    public SchemaContext<T1, T2> Page(int pageCount, int pageIndex)
        {
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
        public SchemaContext(BaseSchemaContext baseContext) : base(baseContext.Connection, baseContext.Sections)
        {

        }


        public SchemaContext<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> expression)
        {
			AddItem<WhereSection>(new SectionItem(expression));
			return this;
        }

        public SchemaContext<T1, T2, T3> GroupBy(Expression<Func<T1, T2, T3, object>> expression)
        {
			AddItem<GroupBySection>(new SectionItem(expression));
			return this;
        }

	    public SchemaContext<T1, T2, T3> Having(Expression<Func<T1, T2, T3, bool>> expression)
	    {
		    AddItem<HavingSection>(new SectionItem(expression));
		    return this;
	    }


	    public SchemaContext<T1, T2, T3> OrderBy<TKey>(Expression<Func<T1, T2, T3, TKey>> expression, bool isAsc = true)
	    {
		    AddItem<OrderBySection>(new SectionItem(expression));
		    return this;
	    }

	    public SchemaContext<T1, T2, T3> Top(int top)
        {
			AddItem<TopSection>(new SectionItem(sectionParameter:new TopSectionParameter(top)));
			return this;
        }

        public SchemaContext<T1, T2, T3> Page(int pageCount, int pageIndex)
        {
			AddItem<PageSection>(new SectionItem(sectionParameter: new PageSectionParameter(pageCount, pageIndex)));
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
