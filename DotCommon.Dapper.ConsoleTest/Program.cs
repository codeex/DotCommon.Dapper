using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotCommon.Dapper.Expressions;
using DotCommon.Dapper.Expressions.Builder;
using DotCommon.Dapper.Expressions.Sections;
using DotCommon.Dapper.Expressions.Translators;
using DotCommon.Dapper.Extensions;
using DotCommon.Dapper.FluentMap;

namespace DotCommon.Dapper.ConsoleTest
{
    class Program
    {
		protected static Dictionary<Type, string> TypeAliasDict = new Dictionary<Type, string>();
		/// <summary>获取属性映射的数据库字段的名称
		/// </summary>
		protected static string GetMapName(PropInfo propInfo)
		{
			var entityMap = FluentMapConfiguration.GetMap(propInfo.Type);
			var propertyMap = entityMap?.PropertyMaps.FirstOrDefault(x => x.PropertyInfo.Name == propInfo.PropName);
			return propertyMap == null ? propInfo.PropName : propertyMap.ColumnName;
		}

		/// <summary>根据类型获取表名称
		/// </summary>
		protected static string GetTableName(Type type)
		{
			var entityMap = FluentMapConfiguration.GetMap(type);
			return entityMap == null ? type.Name : entityMap.TableName;
		}

		/// <summary>获取类型对应表名-->表别名
		/// </summary>
		protected static string GetTypeAlias(Type type)
		{
			var alias = "";
			TypeAliasDict.TryGetValue(type, out alias);
			return alias;
		}

		/// <summary>设置类型对应表名-->表别名 映射
		/// </summary>
		protected static void SetAliasDict(Dictionary<Type, string> aliasDict)
		{
			foreach (var kv in aliasDict)
			{
				TypeAliasDict.Add(kv.Key, kv.Value);
			}
		}

        static void Main(string[] args)
        {
            SetAliasDict(new Dictionary<Type, string>()
            {
                {typeof (Order), "a"},
                {typeof (User), "b"},
                {typeof (Product), "c"}
            });
            //Expression<Func<Order, User, Product, Other>> expr = (x, y, z) =>
            // new Other()
            // {
            //	 Name = y.UserName,
            //	 OId = x.OrderId,
            //	 Code = z.ProductCode
            // };

            //Expression<Func<Order, User, Product, object>> expr = (x, y, z) => new
            //{
            //    OrderId1 = x.OrderId,
            //    Name = y.UserName,
            //    PName = z.ProductName
            //};
            //var translator = new SqlServerSelectTranslator(new TranslatorDelegate(GetTableName, GetMapName, GetTypeAlias));
            //var r = translator.Translate(expr);

            //Expression<Func<Order, Product, object>> expr = (x, y) => x.OrderId;
            //var translator =
            //    new SqlServerOrderByTranslator(new TranslatorDelegate(GetTableName, GetMapName, GetTypeAlias),
            //        new OrderBySectionParameter(false));
            //var r = translator.Translate(expr);

            Expression<Func<Order, Product, User, bool>> expr =
                (x, y, z) => x.OrderId > 3 && y.ProductId.SqlCount()==2;

            var td = new TranslatorDelegate(GetTableName, GetMapName, GetTypeAlias, (x) => true, () => true, () => { },
                (x) => true, (x) => { });
            //var translator =
            //    new SqlServerWhereTranslator(new TranslatorDelegate(GetTableName, GetMapName, GetTypeAlias));

            //var translator =
            //    new SqlServerJoinTranslator(new TranslatorDelegate(GetTableName, GetMapName, GetTypeAlias,(x)=> true),
            //        new JoinSectionParameter(JoinType.InnerJoin));
            var translator = new MySqlHavingTranslator(td);
            var r = translator.Translate(expr);
            Console.WriteLine(r);
            //Console.WriteLine("**************");
           // Console.WriteLine(translator.GetGroupBySelect());
            Console.ReadLine();
        }
    }

	

	public class Other
	{
		public int OId { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }

		public Other()
		{

		}
		public Other(int oId, string name)
		{
			OId = oId;
			Name = name;
		}
	}

	public class Order
	{
		public int OrderId { get; set; }

		public string OrderCode { get; set; }
	}

	public class User
	{
		public int UserId { get; set; }

		public string UserName { get; set; }
	}

	public class Product
	{
		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public string ProductCode { get; set; }
	}
	public class OrderItem
	{
		public int Id { get; set; }

		public int OrderId { get; set; }

		public string OrderCode { get; set; }
	}


}
