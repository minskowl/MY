using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;

namespace KnowledgeBase.Desktop.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public class KeywordCollection : CollectionBase<Keyword>
    {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordCollection"/> class.
        /// </summary>
        public KeywordCollection( KbContext context)
            : base(context,context.ManagerKeyword.GetAll())
        {
           
        }

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Keyword GetById(int id)
        {
            return this.FirstOrDefault(e => e.KeywordID == id);
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="index">The index.</param>
        protected override void RemoveItem(int index)
        {
            Context.ManagerKeyword.Delete(this[index]);
            base.RemoveItem(index);
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            SetContent(Context.ManagerKeyword.GetAll());
        }



    }
}
