using System.Collections.Generic;
using System.Linq;

namespace DotCommon.Dapper.Expressions.Sections
{
    public class QueryWapper 
    {
        public List<Section> Sections { get; }

        public QueryWapper()
        {
            if (Sections == null)
            {
                Sections = new List<Section>();
            }
        }

        /// <summary>Find section
        /// </summary>
        private Section FindSection(SectionType sectionType)
        {
            return Sections?.FirstOrDefault(x => x.SectionType == sectionType);
        }
        /// <summary>Create section
        /// </summary>
        private Section CreateSection(SectionType sectionType, SectionItem sectionItem)
        {
            var section = FindSection(sectionType);
            if (section == null)
            {
                section = new Section(sectionType, sectionItem);
                Sections.Add(section);
            }
            return section;
        }

        /// <summary>Add sectionItem to section,if section dosn't exist create and add.
        /// </summary>
        public QueryWapper AttachSectionItem(SectionType sectionType, SectionItem sectionItem)
        {
            var section = FindSection(sectionType) ?? CreateSection(sectionType, sectionItem);
            section.AttachItem(sectionItem);
            return this;
        }
    }
}
