using System;
using System.Reflection;

namespace DotCommon.Dapper.Expressions
{
    public struct PropInfo
    {
        public Type Type { get; set; }
        public string PropName { get; set; }
        public string KeyName { get; set; }

        public PropInfo(Type type, string propName, string keyName)
        {
            Type = type;
            PropName = propName;
            KeyName = keyName;
        }


        public PropInfo(MemberInfo member)
        {
            Type = member.DeclaringType;
            PropName = member.Name;
            KeyName = member.Name;
        }

        public bool IsNull()
        {
            return Type == null && PropName == null && KeyName == null;
        }

    }
}
