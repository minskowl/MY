using System;
using System.Collections.Generic;

namespace KnowledgeBase.BussinesLayer.Core
{
    /// <summary>
    /// ObjectCopier
    /// </summary>
    public class ObjectCopier
    {
        private KbContext _source;
        private KbContext _destination;
        readonly Dictionary<int, int> _mapCategory = new Dictionary<int, int>();
        readonly Dictionary<int, int> _mapKnowledge = new Dictionary<int, int>();
        /// <summary>
        /// Copies the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="categoryID">The category ID.</param>
        public void Copy(KbContext source, KbContext destination, int categoryID)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (destination == null) throw new ArgumentNullException("destination");

            _source = source;
            _destination = destination;
            CloneCategory(categoryID);

        }

        private void CloneCategory(int categoryID)
        {
            if (categoryID > 0)
            {
                var category = _source.ManagerCategory.GetByID(categoryID);
                if (category == null) return;

                var newCategory = new Category();
                category.Copy(newCategory);
                newCategory.CategoryID = 0;
                if (newCategory.ParentCategoryID.HasValue)
                    newCategory.ParentCategoryID = _mapCategory[newCategory.ParentCategoryID.Value];

                _destination.ManagerCategory.Save(newCategory);

                _mapCategory.Add(category.CategoryID, newCategory.CategoryID);

                var knowledges = _source.ManagerKnowledge.GetByCategoryID(categoryID);
                foreach (var knowledge in knowledges)
                {
                    Clone(knowledge);
                }
            }

            var subCategories = _source.CategoryTree.GetChild(categoryID);
            foreach (var subCategory in subCategories)
            {
                CloneCategory(subCategory);
            }
        }
        private void Clone(Knowledge knowledge)
        {
            var newKnowledge = new Knowledge();
            knowledge.Copy(newKnowledge);

            newKnowledge.KnowledgeID = 0;
            newKnowledge.CategoryID = _mapCategory[newKnowledge.CategoryID];


            var keywords = _source.ManagerKeyword.GetByListID(knowledge.KewordsAssociations);
            _destination.ManagerKnowledge.Save(newKnowledge, GetKeywordsIds(keywords), null);

            _mapKnowledge.Add(knowledge.KnowledgeID, newKnowledge.KnowledgeID);

            var includes = _source.ManagerFileInclude.GetByKnowledgeID(knowledge.KnowledgeID);
            foreach (var fileInclude in includes)
            {
                Clone(fileInclude);
            }

        }
        private void Clone(FileInclude entity)
        {
            var newEntitiy = new FileInclude();
            entity.Copy(newEntitiy);
            newEntitiy.KnowledgeID = _mapKnowledge[newEntitiy.KnowledgeID];
            _destination.ManagerFileInclude.Save(newEntitiy);
            _destination.ManagerFileInclude.SetData(newEntitiy.FileIncludeID, _source.ManagerFileInclude.GetData(entity.FileIncludeID));
        }

        private List<int> GetKeywordsIds(IEnumerable<Keyword> keywords)
        {
            var result = new List<int>();
            foreach (var keyword in keywords)
            {
                var destKey = _destination.ManagerKeyword.GetByName(keyword.Name);
                if (destKey == null)
                {
                    destKey = new Keyword();
                    keyword.Copy(destKey);
                    destKey.KeywordID = 0;
                    _destination.ManagerKeyword.Save(destKey);

                }
                result.Add(destKey.KeywordID);
            }
            return result;
        }

    }
}
