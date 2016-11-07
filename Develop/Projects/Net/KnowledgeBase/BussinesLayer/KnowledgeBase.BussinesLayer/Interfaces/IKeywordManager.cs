using System.Collections.Generic;
using System.Data;

namespace KnowledgeBase.BussinesLayer
{
    public interface IKeywordManager
    {
        /// <summary>
        /// Gets the info all.
        /// </summary>
        /// <returns></returns>
        DataSet GetInfoAll();

        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        IList<Keyword> GetAll();

        /// <summary>
        /// Ges the Keyword by ID.
        /// </summary>
        /// <param name="keywordId">The KeywordID.</param>
        /// <returns></returns>        
        Keyword GetByID(System.Int32 keywordId);

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Keyword GetByName(string name);

        /// <summary>
        /// Gets the by list ID.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        IList<Keyword> GetByListID(ICollection<int> ids);

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IList<Keyword> FindByName(string name);

        /// <summary>
        /// Saves the specified Keyword.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Save(Keyword entity);

        /// <summary>
        /// Sets the status.
        /// </summary>
        /// <param name="keywordId">The keyword ID.</param>
        /// <param name="status">The status.</param>
        void SetStatus(int keywordId, KeywordStatus status);

        /// <summary>
        /// Deletes the specified Keyword.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(Keyword entity);
    }
}