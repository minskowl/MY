
using System;
using System.Web.UI.WebControls;

namespace  Savchin.Web.UI.Calendar
{
    /// <summary>
    /// EditBoxLayout
    /// </summary>
    public class EditBoxLayout : LayoutBase
    {
        private readonly TextBox box = new TextBox();

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public override DateTime? Value
        {
            get
            {
                if (string.IsNullOrEmpty(box.Text))
                    return null;

                try
                {
                    return DateTime.Parse(box.Text);
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value.HasValue)
                    box.Text = value.Value.ToString(DateFormat);
                else
                    box.Text = "";

            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            box.ID = ID + "_Output";
            Controls.Add(box);

            base.OnInit(e);

        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Button.OnClientClick = String.Format("displayCalendar($('{1}'),'{0}',$('{1}'));", DateFormat, box.ClientID);

        }
    }
}