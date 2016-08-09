using System.Collections.Generic;

namespace DotCommon.Dapper.Expressions.Sections
{
    public class Section
    {
        public SectionType SectionType { get; }
        public List<SectionItem> Items { get; }

        public Section(SectionType sectionType) : this(sectionType, new List<SectionItem>())
        {
        }

        public Section(SectionType sectionType, SectionItem item) : this(sectionType, new List<SectionItem>() {item})
        {
        }

        public Section(SectionType sectionType, List<SectionItem> items)
        {
            SectionType = sectionType;
            Items = items;
        }
        


        public void AttachItem(SectionItem item)
        {
            Items.Add(item);
        }

    }

}
