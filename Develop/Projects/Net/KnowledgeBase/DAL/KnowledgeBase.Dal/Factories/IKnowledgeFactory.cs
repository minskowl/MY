using System;
using System.Collections.Generic;
using System.Data;
using KnowledgeBase.DAL;

namespace KnowledgeBase.DAL
{
    public interface IKnowledgeFactory
    {
        /// <summary>
        /// Gets the by public ID.
        /// </summary>
        /// <param name="publicID">The public ID.</param>
        /// <returns></returns>
        KnowledgeValue GetByPublicID(Guid publicID);

        /// <summary>
        /// Searches the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="types">The types.</param>
        /// <param name="categories">The categories.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="statuses">The statuses.</param>
        /// <returns></returns>
        DataSet Search(string text, IList<int> types, IList<int> categories, IList<int> keywords, IList<short> statuses );

        /// <summary>
        /// Gets the short info by category ID.
        /// </summary>
        /// <param name="CategoryID">The category ID.</param>
        /// <returns></returns>
        DataSet GetShortInfoByCategoryID(System.Int32 CategoryID);

        /// <summary>
        /// Saves the keywords associations.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <param name="keywords">The keywords.</param>
        void SaveKeywordsAssociations(int knowledgeID, IEnumerable<int> keywords);

        /// <summary>
        /// Gets the keywords associations.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <returns></returns>
        IList<int> GetKeywordsAssociations(int knowledgeID);

        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        void Insert(KnowledgeValue value);

        /// <summary>
        /// Updates the specified Knowledge.
        /// </summary>
        /// <param name="value">The Knowledge value.</param>
        void Update(KnowledgeValue value);

        /// <summary>
        /// Gets Knowledge by ID.
        /// </summary>
        /// <param name="KnowledgeID">The KnowledgeID.</param>
        /// <returns></returns>
        KnowledgeValue SelectByID(System.Int32 KnowledgeID);

        /// <summary>
        /// Deletes the specified Knowledge.
        /// </summary>
        /// <param name="KnowledgeID">The KnowledgeID.</param>
        void Delete(System.Int32 KnowledgeID);

        /// <summary>
        /// Deletes the specified Knowledge.
        /// </summary>
        /// <param name="value">The value.</param>
        void Delete(KnowledgeValue value);

        /// <summary>
        /// Selects all Knowledge values.
        /// </summary>
        /// <returns>List of all Knowledge</returns>
        IList<KnowledgeValue> SelectAll();

        /// <summary>
        /// Selects Knowledge values CategoryID .
        /// ForeignKey: FK_Knowledges_Categories
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <returns>List of Knowledge</returns>   
        IList<KnowledgeValue> SelectByCategoryID( System.Int32 CategoryID);

        /// <summary>
        /// Selects Knowledge values CreatorID .
        /// ForeignKey: FK_Knowledges_Creator
        /// </summary>
        /// <param name="CreatorID">The CreatorID.</param>
        /// <returns>List of Knowledge</returns>   
        IList<KnowledgeValue> SelectByCreatorID( System.Int32 CreatorID);

        /// <summary>
        /// Selects Knowledge values KnowledgeStatusID .
        /// ForeignKey: FK_Knowledges_KnowledgeStatuses
        /// </summary>
        /// <param name="KnowledgeStatusID">The KnowledgeStatusID.</param>
        /// <returns>List of Knowledge</returns>   
        IList<KnowledgeValue> SelectByKnowledgeStatusID( System.Byte KnowledgeStatusID);

        /// <summary>
        /// Selects Knowledge values KnowledgeTypeID .
        /// ForeignKey: FK_Knowledges_KnowledgeTypes
        /// </summary>
        /// <param name="KnowledgeTypeID">The KnowledgeTypeID.</param>
        /// <returns>List of Knowledge</returns>   
        IList<KnowledgeValue> SelectByKnowledgeTypeID( System.Int16 KnowledgeTypeID);

        /// <summary>
        /// Selects Knowledge values ModificatorID .
        /// ForeignKey: FK_Knowledges_Modificator
        /// </summary>
        /// <param name="ModificatorID">The ModificatorID.</param>
        /// <returns>List of Knowledge</returns>   
        IList<KnowledgeValue> SelectByModificatorID( System.Int32 ModificatorID);
    }
}