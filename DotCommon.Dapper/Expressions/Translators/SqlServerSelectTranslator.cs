using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class SqlServerSelectTranslator : SqlServerQueryTranslator
    {
        public SqlServerSelectTranslator(TranslatorDelegate translatorDelegate) : base(translatorDelegate)
        {

        }

        public override string Translate(Expression expr)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitMember(MemberExpression node)
        {

            return node;
        }

    
    }
}
