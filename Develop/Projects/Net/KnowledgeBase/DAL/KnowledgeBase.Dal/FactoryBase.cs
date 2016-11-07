namespace KnowledgeBase.Dal
{
    /// <summary>
    /// FactoryBase
    /// </summary>
    public abstract class FactoryBase
    {
        protected readonly DalContext Context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryBase"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected FactoryBase(DalContext context)
        {
            Context = context;
        }

    }
}