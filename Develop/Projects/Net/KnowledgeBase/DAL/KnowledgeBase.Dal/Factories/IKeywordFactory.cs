using System.Collections.Generic;
using System.Data;
using KnowledgeBase.DAL;

namespace KnowledgeBase.DAL
{
    public interface IKeywordFactory
    {
        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IList<KeywordValue> FindByName(string name);

        /// <summary>
        /// Gets the info all.
        /// </summary>
        /// <returns></returns>
        DataSet GetInfoAll();

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        KeywordValue GetByName(string name);

        /// <summary>
        /// Sets the status.
        /// </summary>
        /// <param name="keywordID">The keyword ID.</param>
        /// <param name="keywordStatusID">The keyword status ID.</param>
        void SetStatus(int keywordID, byte keywordStatusID);

        /// <summary>
        /// Gets the by list ID.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        IList<KeywordValue> GetByListID(ICollection<int> ids);

        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        void Insert(KeywordValue value);

        /// <summary>
        /// Updates the specified Keyword.
        /// </summary>
        /// <param name="value">The Keyword value.</param>
        void Update(KeywordValue value);

        /// <summary>
        /// Gets Keyword by ID.
        /// </summary>
        /// <param name="KeywordID">The KeywordID.</param>
        /// <returns></returns>
        KeywordValue SelectByID(System.Int32 KeywordID);

        /// <summary>
        /// Deletes the specified Keyword.
        /// </summary>
        /// <param name="KeywordID">The KeywordID.</param>
        void Delete(System.Int32 KeywordID);

        /// <summary>
        /// Deletes the specified Keyword.
        /// </summary>
        /// <param name="value">The value.</param>
        void Delete(KeywordValue value);

        /// <summary>
        /// Selects all Keyword values.
        /// </summary>
        /// <returns>List of all Keyword</returns>
        IList<KeywordValue> SelectAll();

        /// <summary>
        /// Selects Keyword values KeywordStatusID .
        /// ForeignKey: FK_Keywords_KeywordStatuses
        /// </summary>
        /// <param name="KeywordStatusID">The KeywordStatusID.</param>
        /// <returns>List of Keyword</returns>   
        IList<KeywordValue> SelectByKeywordStatusID(System.Byte KeywordStatusID);

        /// <summary>
        /// Selects Keyword values KeywordTypeID .
        /// ForeignKey: FK_Keywords_KeywordTypes
        /// </summary>
        /// <param name="KeywordTypeID">The KeywordTypeID.</param>
        /// <returns>List of Keyword</returns>   
        IList<KeywordValue> SelectByKeywordTypeID(System.Int16 KeywordTypeID);
    }
}