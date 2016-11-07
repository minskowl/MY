#region Version & Copyright
/* 
 * $Id: DropPanelButton.cs 19933 2007-08-14 13:48:38Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsDropPanelButton, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]



namespace Savchin.Web.UI
{

    internal static partial class EmbeddedResources
    {
        internal const string JsDropPanelButton = namespaceName + "DropPanelButton.js";
    }
    [ParseChildren(false), PersistChildren(true)]
    public class DropPanelButton : WebControl
    {
        private ButtonEx button = new ButtonEx();
        private Panel panel = new Panel();

        #region Properties
        /// <summary>
        /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> value that corresponds to this Web server control. This property is used primarily by control developers.
        /// </summary>
        /// <value></value>
        /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> enumeration values.</returns>
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Span; }
        }
        /// <summary>
        /// Gets or sets the panel.
        /// </summary>
        /// <value>The panel.</value>
        [Category("Appearance"), Description("Panel")]
        public Panel Panel
        {
            get { return panel; }
            set { panel = value; }
        }

        /// <summary>
        /// Gets or sets the button.
        /// </summary>
        /// <value>The button.</value>
        [Category("Appearance"), Description("Button")]
        public ButtonEx Button
        {
            get { return button; }
            set { button = value; }
        }

        /// <summary>
        /// Gets the name of the JS object.
        /// </summary>
        /// <value>The name of the JS object.</value>
        public string JSObjectName
        {
            get { return ClientID + "Obj"; }
        }

        /// <summary>
        /// Gets or sets the background color of the Web server control.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Drawing.Color"/> that represents the background color of the control. The default is <see cref="F:System.Drawing.Color.Empty"/>, which indicates that this property is not set.</returns>

        public override Color BackColor
        {
            get { return button.BackColor; }
            set { button.BackColor = value; }
        }


        /// <summary>
        /// Gets or sets the border color of the Web control.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Drawing.Color"/> that represents the border color of the control. The default is <see cref="F:System.Drawing.Color.Empty"/>, which indicates that this property is not set.</returns>
        public override Color BorderColor
        {
            get { return button.BorderColor; }
            set { button.BorderColor = value; }
        }



        /// <summary>
        /// Gets or sets the border style of the Web server control.
        /// </summary>
        /// <value></value>
        /// <returns>One of the <see cref="T:System.Web.UI.WebControls.BorderStyle"/> enumeration values. The default is NotSet.</returns>
        public override BorderStyle BorderStyle
        {
            get { return button.BorderStyle; }
            set { button.BorderStyle = value; }
        }



        /// <summary>
        /// Gets or sets the border width of the Web server control.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Web.UI.WebControls.Unit"/> that represents the border width of a Web server control. The default value is <see cref="F:System.Web.UI.WebControls.Unit.Empty"/>, which indicates that this property is not set.</returns>
        public override Unit BorderWidth
        {
            get { return button.BorderWidth; }
            set { button.BorderWidth = value; }
        }

        /// <summary>
        /// Gets a <see cref="T:System.Web.UI.ControlCollection"/> object that represents the child controls for a specified server control in the UI hierarchy.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The collection of child controls for the specified server control.
        /// </returns>
        public override ControlCollection Controls
        {
            get { return panel.Controls; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DropPanelButton"/> class.
        /// </summary>
        public DropPanelButton()
        {

            button.UseSubmitBehavior = false;
            button.Text = "Button";

            panel.Height = 30;
            panel.Width = 50;
            panel.CssClass = "DropPanelButton";
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            EnsureChildControls();
        }
        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            button.ID = ID + "Button";
            panel.ID = ID + "Panel";

            base.Controls.Add(Button);
            base.Controls.Add(panel);
        }


        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            button.OnClientClick = JSObjectName + ".Show($('" + button.ClientID + "'),$('" + panel.ClientID + "'));";

            panel.Attributes.CssStyle.Add(HtmlTextWriterStyle.Position, "absolute");
            panel.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
            panel.Attributes.Add("buttonId", button.ClientID);
            panel.Attributes.Add("single", "true");

            //Note: Test JAVASscript
            //Page.ClientScript.RegisterClientScriptInclude(typeof(DropPanelButton), "DropPanelButton.js", AppSettings.ApplicationJsPath + "Test.js");

            Page.ClientScript.RegisterClientScriptResource(typeof(DropPanelButton), EmbeddedResources.JsDropPanelButton);

            string initscript = String.Format("\n var {0} = new DropPanelButton('{1}','{2}','{3}');\n",
                                              JSObjectName,
                                              ClientID,
                                              panel.ClientID,
                                              button.ClientID);

            Page.ClientScript.RegisterStartupScript(typeof(DropPanelButton),
                                                    "DropPanelButton" + ClientID,
                                                    initscript,
                                                    true);
        }
    }
}
