namespace DotCommon.Dapper.Expressions.Translators
{
    public abstract class MySqlQueryTranslator : MySqlTranslator
    {
        protected TranslatorDelegate TranslatorDelegate { get; private set; }

        protected MySqlQueryTranslator(TranslatorDelegate translatorDelegate)
        {
            TranslatorDelegate = translatorDelegate;
        }
    }
}
