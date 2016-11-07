#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

namespace KnowledgeBase.Controls
{
    public class HelpMovieButton : MoveToButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpMovieButton"/> class.
        /// </summary>
        public HelpMovieButton()
        {
            UseSubmitBehavior = false;
            Target = "_blank";
        }
    }
}
