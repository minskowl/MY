using System.Collections.Generic;
using System.Linq;
using Advertiser.Entities;

namespace Advertiser.Views
{
    /// <summary>
    /// EntityListView
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityListView<T> : ItemListView<T>
        where T : class, IEntity, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityListView&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="source">The source.</param>
        protected EntityListView(string name, List<T> source) : base(name, source)
        {
        }

        protected override void OnAddItem()
        {
            SelectedItem = new T { Id = GetNewId() };
        }

        /// <summary>
        /// Gets the new id.
        /// </summary>
        /// <returns></returns>
        protected int GetNewId()
        {
            try
            {
                return Items.Max(e => e.Id) + 1;
            }
            catch
            {
                return 1;
            }
        }
    }
}