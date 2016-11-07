using System.Collections.Generic;
using System.Linq;
using Savchin.Core;

namespace KnowledgeBase.BussinesLayer.Core
{
    /// <summary>
    /// ManagerBases
    /// </summary>
    /// <typeparam name="TFactory">The type of the factory.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TEntityValue">The type of the entity value.</typeparam>
    public class ManagerBase<TFactory, TEntity, TEntityValue>
        where TFactory : class
        where TEntity : EntityBase<TEntityValue>, new()
        where TEntityValue : class, ICopiable, new()
    {

        /// <summary>
        /// Context
        /// </summary>
        protected readonly KbContext Context;
        protected readonly TFactory Factory;


        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="factory">The factory.</param>
        protected ManagerBase(KbContext context,TFactory factory)
        {
            Context = context;
            Factory = factory;
        }

        /// <summary>
        /// Wraps the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        protected List<TEntity> Wrap(IEnumerable<TEntityValue> values)
        {
            return values.Select(Wrap).ToList();
        }

        /// <summary>
        /// Wraps the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected TEntity Wrap(TEntityValue value)
        {
            return (value == null) ? null : new TEntity
            {
                ObjectValue = value,
                Context = Context
            };
        }

       
    }
}
