using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using KnowledgeBase.BussinesLayer.Core;

namespace KnowledgeBase.Desktop.Collections
{
    /// <summary>
    /// CollectionBase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectionBase<T> : ObservableCollection<T>
    {

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        protected KbContext Context { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected CollectionBase(KbContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="items">The items.</param>
        protected CollectionBase(KbContext context, IEnumerable<T> items)
            : base(items)
        {
            Context = context;
        }

        /// <summary>
        /// Sets the content.
        /// </summary>
        /// <param name="items">The items.</param>
        protected void SetContent(IEnumerable<T> items)
        {   
            Clear();

            foreach (var item in items)
            {
                Add(item);
            }
        }
    }
}
