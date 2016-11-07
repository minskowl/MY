using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Desktop.Collections;

namespace KnowledgeBase.Desktop.Core
{
    /// <summary>
    /// Workspace
    /// </summary>
    public class Workspace
    {

        /// <summary>
        /// Gets or sets the user file controller.
        /// </summary>
        /// <value>The user file controller.</value>
        public UserFileCollection UserFiles { get; private set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>The keywords.</value>
        public KeywordCollection Keywords { get; private set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public CategoriesCollection Categories { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Workspace"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public  Workspace(KbContext context)
        {
            Keywords = new KeywordCollection(context);
            UserFiles = new UserFileCollection(KbContext.CurrentUserId, context);
            Categories = new CategoriesCollection(context);
        }
    }
}
