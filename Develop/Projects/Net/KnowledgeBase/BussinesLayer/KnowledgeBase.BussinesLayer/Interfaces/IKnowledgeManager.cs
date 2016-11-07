using System;
using System.Collections.Generic;
using System.Data;

namespace KnowledgeBase.BussinesLayer
{
    public interface IKnowledgeManager
    {
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        IList<Knowledge> GetAll();

        /// <summary>
        /// Ges the Knowledge by ID.
        /// </summary>
        /// <param name="KnowledgeID">The KnowledgeID.</param>
        /// <returns></returns>        
        Knowledge GetByID(System.Int32 KnowledgeID);

        /// <summary>
        /// Gets the by public ID.
        /// </summary>
        /// <param name="publicID">The public ID.</param>
        /// <returns></returns>
        Knowledge GetByPublicID(Guid publicID);

        /// <summary>
        /// Gets the by category ID.
        /// </summary>
        /// <param name="CategoryID">The category ID.</param>
        /// <returns></returns>
        IList<Knowledge> GetByCategoryID(System.Int32 CategoryID);

        /// <summary>
        /// Get Knowledge values KnowledgeTypeID .
        /// </summary>
        /// <param name="KnowledgeTypeID">The KnowledgeTypeID.</param>
        /// <returns>List of Knowledge</returns>   
        IList<Knowledge> GetByKnowledgeTypeID(System.Int16 KnowledgeTypeID);

        /// <summary>
        /// Get Knowledge values CreatorID .
        /// </summary>
        /// <param name="CreatorID">The CreatorID.</param>
        /// <returns>List of Knowledge</returns>   
        IList<Knowledge> GetByCreatorID(System.Int32 CreatorID);

        /// <summary>
        /// Get Knowledge values ModificatorID .
        /// </summary>
        /// <param name="ModificatorID">The ModificatorID.</param>
        /// <returns>List of Knowledge</returns>   
        IList<Knowledge> GetByModificatorID(System.Int32 ModificatorID);

        /// <summary>
        /// Adds the can view column.
        /// </summary>
        /// <param name="data">The data.</param>
        void AddCanViewColumn(DataTable data);

        /// <summary>
        /// Removes the not viewable.
        /// </summary>
        /// <param name="result">The result.</param>
        void RemoveNotViewable(DataTable result);

        /// <summary>
        /// Gets the short info by category ID.
        /// </summary>
        /// <param name="categoryID">The category ID.</param>
        /// <returns></returns>
        DataTable GetShortInfoByCategoryID(int categoryID);

        /// <summary>
        /// Adds the can edit column.
        /// </summary>
        /// <param name="data">The data.</param>
        void AddCanEditColumn(DataTable data);

        /// <summary>
        /// Searches the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="types">The types.</param>
        /// <param name="categories">The categories.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="statuses">The statuses.</param>
        /// <returns></returns>
        DataTable Search(string text, IList<KnowledgeType> types, IList<int> categories,
                                         IList<int> keywords, IList<KnowledgeStatus> statuses);

        /// <summary>
        /// Saves the specified Knowledge.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="newKeywords">The new keywords.</param>
        void Save(Knowledge entity, IList<int> keywords, IEnumerable<string> newKeywords);

        /// <summary>
        /// Gets the keywords associations.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <returns></returns>
        IList<int> GetKeywordsAssociations(int knowledgeID);

        /// <summary>
        /// Deletes the specified Knowledge.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(Knowledge entity);
    }
}