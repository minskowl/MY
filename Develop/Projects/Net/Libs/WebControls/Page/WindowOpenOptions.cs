using System.Text;

namespace Savchin.Web.UI
{
    /// <summary>
    /// WindowOpenOptions
    /// </summary>
    public class WindowOpenOptions
    {
        #region Size
        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>The left.</value>
        public int? Left { get; set; }
        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        /// <value>The top.</value>
        public int? Top { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int? Width { get; set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int? Height { get; set; } 
        #endregion


        #region Options
        /// <summary>
        /// Gets or sets the fullscreen.
        /// </summary>
        /// <value>The fullscreen.</value>
        public bool? Fullscreen { get; set; }

        /// <summary>
        /// Gets or sets the menubar.
        /// </summary>
        /// <value>The menubar.</value>
        public bool? Menubar { get; set; }

        /// <summary>
        /// Gets or sets the scroll bars.
        /// </summary>
        /// <value>The scroll bars.</value>
        public bool? ScrollBars { get; set; }

        /// <summary>
        /// Gets or sets the resizable.
        /// </summary>
        /// <value>The resizable.</value>
        public bool? Resizable { get; set; } 

        #endregion

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {

            var builder = new StringBuilder();
            if (Left.HasValue)
                builder.Append("left=" + Left.Value + ",");
            if (Top.HasValue)
                builder.Append("top=" + Left.Value + ",");
            if (Width.HasValue)
                builder.Append("width=" + Left.Value + ",");
            if (Height.HasValue)
                builder.Append("height=" + Left.Value + ",");
            if (Fullscreen.HasValue)
                builder.Append("fullscreen=" + (Fullscreen.Value ? 1 : 0) + ",");
            if (Menubar.HasValue)
                builder.Append("menubar=" + (Menubar.Value ? 1 : 0) + ",");
            if (ScrollBars.HasValue)
                builder.Append("scrollbars =" + (ScrollBars.Value ? 1 : 0) + ",");
            if (Resizable.HasValue)
                builder.Append("resizable =" + (Resizable.Value ? 1 : 0) + ",");

            return builder.Length > 0 ? builder.ToString(0, builder.Length - 1) : null;
        }
    }
}
