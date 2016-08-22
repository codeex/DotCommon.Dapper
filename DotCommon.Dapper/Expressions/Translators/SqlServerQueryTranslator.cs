using System;
using System.Reflection;

namespace DotCommon.Dapper.Expressions.Translators
{
    public abstract class SqlServerQueryTranslator : SqlServerTranslator
    {
        protected TranslatorDelegate TranslatorDelegate { get; private set; }

        protected SqlServerQueryTranslator(TranslatorDelegate translatorDelegate)
        {
            TranslatorDelegate = translatorDelegate;
        }
    }
}
