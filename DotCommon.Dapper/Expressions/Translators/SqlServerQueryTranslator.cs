using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public abstract class SqlServerQueryTranslator : SqlServerTranslator
    {
        protected TranslatorDelegate TranslatorDelegate { get; private set; }
        protected ISectionParameter Parameter { get; }

        protected SqlServerQueryTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter=null)
        {
            TranslatorDelegate = translatorDelegate;
            Parameter = parameter;
        }
    }
}
