using DotCommon.Dapper.Expressions.Sections;

namespace DotCommon.Dapper.Expressions.Translators
{
    public abstract class MySqlQueryTranslator : MySqlTranslator
    {
        protected TranslatorDelegate TranslatorDelegate { get; private set; }
        protected ISectionParameter Parameter { get; }

        protected MySqlQueryTranslator(TranslatorDelegate translatorDelegate, ISectionParameter parameter = null)
        {
            TranslatorDelegate = translatorDelegate;
            Parameter = parameter;
        }
    }
}
