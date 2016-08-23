using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotCommon.Dapper.Expressions.Translators
{
	public class SqlServerHavingTranslator : SqlServerQueryTranslator
	{
		public SqlServerHavingTranslator(TranslatorDelegate translatorDelegate) : base(translatorDelegate)
		{
		}

		public override string Translate(LambdaExpression expr)
		{
			Visit(expr.Body);
			return SqlBuilder.ToString();
		}


	}
}
