#region Version & Copyright
/* 
 * $Id: LayoutBase.cs 19239 2007-07-26 12:35:13Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI.Calendar
{
    /// <summary>
    /// LayoutBase
    /// </summary>
    public abstract class LayoutBase : WebControl
    {
        #region Properties

        /// <summary>
        /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> value that corresponds to this Web server control. This property is used primarily by control developers.
        /// </summary>
        /// <value></value>
        /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> enumeration values.</returns>
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Div; }
        }

        private ButtonEx button;
        /// <summary>
        /// Gets the button.
        /// </summary>
        /// <value>The button.</value>
        public ButtonEx Button
        {
            get { return button; }
        }
        private string dateFormat;
        /// <summary>
        /// Gets or sets the date format.
        /// </summary>
        /// <value>The date format.</value>
        public string DateFormat
        {
            get { return dateFormat; }
            set { dateFormat = value; }
        }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public abstract DateTime? Value
        {
            get;
            set;
        }
        #endregion




        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            button = new ButtonEx
                         {
                             CausesValidation = false,
                             UseSubmitBehavior = false,
                             ID = (ID + "_Button"),
                             Text = "*",
                             Mode = ButtonEx.ButtonType.Button,
                             CssClass = "btn"
                         };
            button.CausesValidation = false;
            button.UseSubmitBehavior = false;

            Controls.Add(button);

        }
    }
}