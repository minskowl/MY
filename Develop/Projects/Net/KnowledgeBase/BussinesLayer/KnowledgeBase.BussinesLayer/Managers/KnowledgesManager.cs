using System;
using System.Collections.Generic;
using System.Data;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Core;
using KnowledgeBase.DAL;
using Savchin.Collection.Generic;
using Savchin.Data.Common;

namespace KnowledgeBase.BussinesLayer.Managers
{

    /// <summary>
    /// Knowledge Manager class
    ///</summary>
    public class KnowledgeManager : ManagerBase< IKnowledgeFactory, Knowledge, KnowledgeValue>, IKnowledgeManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeManager"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        public KnowledgeManager(KbContext context, IFactoryProvider provider)
            : base(context,provider.CreateKnowledgeFactory())
        {
         
        }

        #region Getters

        #region Entitites
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        public IList<Knowledge> GetAll()
        {
            return Wrap(Factory.SelectAll());
        }
        /// <summary>
        /// Ges the Knowledge by ID.
        /// </summary>
        /// <param name="KnowledgeID">The KnowledgeID.</param>
        /// <returns></returns>        
        public Knowledge GetByID(System.Int32 KnowledgeId)
        {
            return Wrap(Factory.SelectByID(KnowledgeId));
        }

        /// <summary>
        /// Gets the by public ID.
        /// </summary>
        /// <param name="publicID">The public ID.</param>
        /// <returns></returns>
        public Knowledge GetByPublicID(Guid publicId)
        {
            return Wrap(Factory.GetByPublicID(publicId));
        }

        /// <summary>
        /// Gets the by category ID.
        /// </summary>
        /// <param name="CategoryID">The category ID.</param>
        /// <returns></returns>
        public IList<Knowledge> GetByCategoryID(System.Int32 CategoryId)
        {
            return Wrap(Factory.SelectByCategoryID(CategoryId));
        }

        /// <summary>
        /// Get Knowledge values KnowledgeTypeID .
        /// </summary>
        /// <param name="KnowledgeTypeID">The KnowledgeTypeID.</param>
        /// <returns>List of Knowledge</returns>   
        public IList<Knowledge> GetByKnowledgeTypeID(System.Int16 KnowledgeTypeId)
        {
            return Wrap(Factory.SelectByKnowledgeTypeID(KnowledgeTypeId));
        }

        /// <summary>
        /// Get Knowledge values CreatorID .
        /// </summary>
        /// <param name="CreatorID">The CreatorID.</param>
        /// <returns>List of Knowledge</returns>   
        public IList<Knowledge> GetByCreatorID(System.Int32 CreatorId)
        {
            return Wrap(Factory.SelectByCreatorID(CreatorId));
        }

        /// <summary>
        /// Get Knowledge values ModificatorID .
        /// </summary>
        /// <param name="ModificatorID">The ModificatorID.</param>
        /// <returns>List of Knowledge</returns>   
        public IList<Knowledge> GetByModificatorID(System.Int32 ModificatorId)
        {
            return Wrap(Factory.SelectByModificatorID(ModificatorId));

        }
        #endregion

        #region DataSet

        /// <summary>
        /// Adds the can view column.
        /// </summary>
        /// <param name="data">The data.</param>
        public void AddCanViewColumn(DataTable data)
        {
            var indexCanView = data.Columns.Add("CanView", typeof(bool)).Ordinal;
            var indexCategoryId = data.Columns["CategoryID"].Ordinal;

            foreach (DataRow row in data.Rows)
            {
                var categoryId = (int)row[indexCategoryId];
                row[indexCanView] = Context.GetPermissionsForCategory(categoryId).CanViewCategory();
            }
        }
        /// <summary>
        /// Removes the not viewable.
        /// </summary>
        /// <param name="result">The result.</param>
        public void RemoveNotViewable(DataTable result)
        {
            result.RemoveRows(delegate(DataRow row)
                                  {
                                      var status = (KnowledgeStatus)row["KnowledgeStatusID"];
                                      var categoryId = (int)row["CategoryID"];
                                      var permission = Context.GetPermissionsForCategory(categoryId);
                                      return permission.CanViewKnowledge(status);
                                  });
        }

        /// <summary>
        /// Gets the short info by category ID.
        /// </summary>
        /// <param name="categoryID">The category ID.</param>
        /// <returns></returns>
        public DataTable GetShortInfoByCategoryID(int categoryId)
        {
            return Factory.GetShortInfoByCategoryID(categoryId).Tables[0];
        }

        /// <summary>
        /// Adds the can edit column.
        /// </summary>
        /// <param name="data">The data.</param>
        public void AddCanEditColumn(DataTable data)
        {

            var indexCanEdit = data.Columns.Add("CanEdit", typeof(bool)).Ordinal;
            var indexCategoryId = data.Columns["CategoryID"].Ordinal;

            foreach (DataRow row in data.Rows)
            {
                var categoryId = (int)row[indexCategoryId];
                var status = (KnowledgeStatus)row["KnowledgeStatusID"];
                var creatorId = (int)row["CreatorID"];

                var permission = Context.GetPermissionsForCategory(categoryId);
                row[indexCanEdit] = permission.CanEditKnowledge(status, creatorId);
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// Searches the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="types">The types.</param>
        /// <param name="categories">The categories.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="statuses">The statuses.</param>
        /// <returns></returns>
        public DataTable Search(string text, IList<KnowledgeType> types, IList<int> categories,
            IList<int> keywords, IList<KnowledgeStatus> statuses)
        {
            return Factory.Search(
                            text,
                            CollectionUtil.Convert<int>(types),
                            categories,
                            keywords,
                            CollectionUtil.Convert<short>(statuses)
                ).Tables[0];
        }

        /// <summary>
        /// Saves the specified Knowledge.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="newKeywords">The new keywords.</param>
        public virtual void Save(Knowledge entity, IList<int> keywords, IEnumerable<string> newKeywords)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            if (keywords == null) throw new ArgumentNullException("keywords");

            if (entity.Context == null)
                entity.Context = Context;

            entity.Validate();

            Context.RequirePermission(entity.CategoryID, Permission.Publish);

            Context.BeginTransaction();
            try
            {
                var keywordManager = Context.ManagerKeyword;
                if (newKeywords != null)
                    foreach (var keywordName in newKeywords)
                    {
                        var keyword = keywordManager.GetByName(keywordName);
                        if (keyword == null)
                        {
                            keyword = new Keyword { Name = keywordName };
                            keywordManager.Save(keyword);
                        }
                        keywords.Add(keyword.KeywordID);
                    }

                if (Identifier.IsValid(entity.KnowledgeID))
                {
                    entity.ModificatorID = KbContext.CurrentUserId;
                    entity.ModificationDate = DateTime.Now;
                    Factory.Update(entity.ObjectValue);

                }
                else
                {
                    entity.CreationDate = DateTime.Now;
                    entity.CreatorID = KbContext.CurrentUserId;
                    Factory.Insert(entity.ObjectValue);
                }

                Factory.SaveKeywordsAssociations(entity.KnowledgeID, keywords);

                Context.CommitTransaction();
            }
            catch
            {
                Context.RollbackTransaction();
                throw;
            }
        }


        /// <summary>
        /// Gets the keywords associations.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <returns></returns>
        public IList<int> GetKeywordsAssociations(int knowledgeId)
        {
            return Factory.GetKeywordsAssociations(knowledgeId);
        }

        /// <summary>
        /// Deletes the specified Knowledge.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(Knowledge entity)
        {

            var permission = Context.RequirePermission(entity.CategoryID, Permission.Moderate);
            if (permission.HasPermission(Permission.Admin))
            {
                Factory.Delete(entity.ObjectValue);
            }
            else
            {
                entity.KnowledgeStatus = KnowledgeStatus.Deleted;
                Factory.Update(entity.ObjectValue);
            }
        }

    }
}