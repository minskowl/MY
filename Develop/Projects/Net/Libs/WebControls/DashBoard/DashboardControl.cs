#region Version & Copyright
/* 
 * $Id: DashboardControl.cs 20109 2007-08-17 07:44:02Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class DashboardControl : UserControl, IDashboard
    {
        readonly Dashboard instance = new Dashboard();

        #region Properties


        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Title
        {
            get { return instance.Title; }

            set { instance.Title = value; }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        [DefaultValue(typeof(Unit), "100"),
         Category("Layout"),
         Description("Height")]
        public virtual Unit Height
        {
            get { return instance.ContentHeight; }
            set { instance.ContentHeight = value; }
        }


        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        [Category("Layout"),
        Description("Max Height")
        ]
        public virtual Unit MaxHeight
        {
            get { return instance.MaxHeight; }
            set { instance.MaxHeight = value; }
        }
        /// <summary>
        /// Gets the name of the JS object.
        /// </summary>
        /// <value>The name of the JS object.</value>
        public string JSObjectName
        {
            get { return instance.JSObjectName; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [status bar visible].
        /// </summary>
        /// <value><c>true</c> if [status bar visible]; otherwise, <c>false</c>.</value>
        [Category("Layout")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool StatusBarVisible
        {
            get { return instance.StatusBarVisible; }
            set { instance.StatusBarVisible = value; }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public DashboardSettings Settings
        {
            get { return instance.Settings; }
        }


        /// <summary>
        /// Gets or sets the scroll bars.
        /// </summary>
        /// <value>The scroll bars.</value>
        [Category("Layout"), DefaultValue(0), Description("Panel_ScrollBars")]
        public virtual ScrollBars ScrollBars
        {
            get { return instance.ScrollBars; }
            set { instance.ScrollBars = value; }
        }

        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            instance.ID = ID + "DashBoard";
            Controls.Add(instance);
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(System.EventArgs e)
        {
           // instance.SetJSObjectName(ClientID + "Obj");
            base.OnPreRender(e);
        }

        /// <summary>
        /// Notifies the server control that an element, either XML or HTML, was parsed, and adds the element to the server control's <see cref="T:System.Web.UI.ControlCollection"/> object.
        /// </summary>
        /// <param name="obj">An <see cref="T:System.Object"/> that represents the parsed element.</param>
        protected override void AddParsedSubObject(object obj)
        {
            if (obj != null && obj is Control)
                instance.Container.Controls.Add((Control)obj);
        }



    }
}
