using System;
using System.Collections.Generic;
using System.Data;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core;
using KnowledgeBase.DAL;

namespace KnowledgeBase.BussinesLayer.Managers
{

    /// <summary>
    /// Keyword Manager class
    ///</summary>
    public class KeywordManager : ManagerBase<IKeywordFactory, Keyword, KeywordValue>, IKeywordManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordManager"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="provider">The provider.</param>
        public KeywordManager(KbContext context, IFactoryProvider provider)
            : base(context, provider.CreateKeywordFactory())
        {

        }

        /// <summary>
        /// Gets the info all.
        /// </summary>
        /// <returns></returns>
        public DataSet GetInfoAll()
        {
            return Factory.GetInfoAll();
        }

        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        public IList<Keyword> GetAll()
        {
            return Wrap(Factory.SelectAll());
        }
        /// <summary>
        /// Ges the Keyword by ID.
        /// </summary>
        /// <param name="keywordId">The KeywordID.</param>
        /// <returns></returns>        
        public Keyword GetByID(Int32 keywordId)
        {
            return Wrap(Factory.SelectByID(keywordId));
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Keyword GetByName(string name)
        {
            return Wrap(Factory.GetByName(name));
        }

        /// <summary>
        /// Gets the by list ID.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public IList<Keyword> GetByListID(ICollection<int> ids)
        {
            return Wrap(Factory.GetByListID(ids));
        }

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IList<Keyword> FindByName(string name)
        {
            return Wrap(Factory.FindByName(name));
        }


        /// <summary>
        /// Saves the specified Keyword.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Save(Keyword entity)
        {
            entity.Validate();
            if (Identifier.IsValid(entity.KeywordID))
            {
                Factory.Update(entity.ObjectValue);
            }
            else
            {
                var value = entity.ObjectValue;
                if (value.KeywordTypeID == 0) value.KeywordTypeID = (short)KeywordType.Other;
                Factory.Insert(value);
            }
        }


        /// <summary>
        /// Sets the status.
        /// </summary>
        /// <param name="keywordId">The keyword ID.</param>
        /// <param name="status">The status.</param>
        public void SetStatus(int keywordId, KeywordStatus status)
        {
            Factory.SetStatus(keywordId, (byte)status);
        }

        /// <summary>
        /// Deletes the specified Keyword.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(Keyword entity)
        {
            Factory.Delete(entity.ObjectValue);
        }

        /// <summary>
        /// Wraps the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        private IList<Keyword> Wrap(ICollection<KeywordValue> values)
        {
            List<Keyword> result = new List<Keyword>(values.Count);
            foreach (KeywordValue value in values)
            {
                result.Add(new Keyword(value));
            }
            return result;
        }

        /// <summary>
        /// Wraps the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private Keyword Wrap(KeywordValue value)
        {
            return (value == null) ? null : new Keyword(value);
        }
    }
}