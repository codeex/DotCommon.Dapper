using System;
using System.Reflection;

namespace DotCommon.Dapper.Expressions.Translators
{
    public class TranslatorDelegate
    {
        public Func<Type, string> GetTableNameDelegate { get; private set; }

        public Func<MemberInfo, string> GetMemberMapDelegate { get; private set; }

        public Func<Type, string> GetTypeAlias { get; private set; }

        public Func<OneMoreType, bool> GetTypeOneMore { get; private set; }

        public Action<OneMoreType> SetTypeOneMore { get; private set; }

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

        public TranslatorDelegate(Func<Type, string> getTableNameDelegate, Func<MemberInfo, string> getMemberMapDelegate,
            Func<Type, string> getTypeAlias, Func<OneMoreType, bool> getTypeOneMore, Action<OneMoreType> setTypeOneMore)
        {
            GetTableNameDelegate = getTableNameDelegate;
            GetMemberMapDelegate = getMemberMapDelegate;
            GetTypeAlias = getTypeAlias;
            GetTypeOneMore = getTypeOneMore;
            SetTypeOneMore = setTypeOneMore;
        }
    }
}
