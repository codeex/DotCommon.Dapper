using System.Reflection;

namespace DotCommon.Dapper.FluentMap
{
    public interface IPropertyMap
    {
        string ColumnName { get; }
        PropertyInfo PropertyInfo { get; }
        bool CaseSensitive { get; }
        bool Ignored { get; }

        IPropertyMap Column(string columnName);
        IPropertyMap Ignore();
        IPropertyMap Case();
    }

    public class PropertyMap : IPropertyMap
    {
        public PropertyMap(PropertyInfo info)
        {
            PropertyInfo = info;
        }

        public IPropertyMap Column(string columnName)
        {
            ColumnName = columnName;
            return this;
        }

        public IPropertyMap Case()
        {
            CaseSensitive = true;
            return this;
        }

        public IPropertyMap Ignore()
        {
            Ignored = true;
            return this;
        }


        public string ColumnName { get; private set; }
        public bool CaseSensitive { get; private set; }

        public bool Ignored { get; private set; }

        public PropertyInfo PropertyInfo { get; }
    }
}
