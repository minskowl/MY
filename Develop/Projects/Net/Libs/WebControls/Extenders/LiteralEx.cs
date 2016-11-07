using System.Web.UI.WebControls;
using Savchin.Text;

namespace Savchin.Web.UI
{
    public class LiteralEx : Literal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralEx"/> class.
        /// </summary>
        public LiteralEx()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralEx"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public LiteralEx(string text)
        {
            Text = text;
        }


        /// <summary>
        /// Creates the new line.
        /// </summary>
        /// <returns></returns>
        public static LiteralEx CreateNewLine()
        {
            return new LiteralEx("</br>");
        }
        /// <summary>
        /// Creates the space.
        /// </summary>
        /// <returns></returns>
        public static LiteralEx CreateSpace()
        {
            return new LiteralEx("&nbsp;");
        }
        /// <summary>
        /// Creates the space.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static LiteralEx CreateSpace(int count)
        {
            return new LiteralEx(StringUtil.Clone( "&nbsp",count));
        }
    }
}
