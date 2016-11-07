#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    /// <summary>
    /// PDFButton
    /// </summary>
    public class PDFButton : ButtonEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDFButton"/> class.
        /// </summary>
        public PDFButton()
        {
            Mode = ButtonType.Link;
            ImageUrl = ImagePathProvider.PdfImage;
        }
        
    }
}
