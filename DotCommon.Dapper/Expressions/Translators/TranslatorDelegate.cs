using System;
using System.Reflection;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class TranslatorDelegate
    {
        public Func<Type, string> GetTableNameDelegate { get; private set; }

        public Func<MemberInfo, string> GetMemberMapDelegate { get; private set; }

		public Func<Type,string> GetTypeAlias { get; private set; }

		public TranslatorDelegate()
        {

        }

        public TranslatorDelegate(Func<Type, string> getTableNameDelegate, Func<MemberInfo, string> getMemberMapDelegate)
        {
            GetTableNameDelegate = getTableNameDelegate;
            GetMemberMapDelegate = getMemberMapDelegate;
        }

	    public TranslatorDelegate(Func<Type, string> getTableNameDelegate, Func<MemberInfo, string> getMemberMapDelegate,
		    Func<Type, string> getTypeAlias)
	    {
		    GetTableNameDelegate = getTableNameDelegate;
		    GetMemberMapDelegate = getMemberMapDelegate;
		    GetTypeAlias = getTypeAlias;
	    }
    }
}
