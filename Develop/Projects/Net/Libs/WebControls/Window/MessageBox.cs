#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Savchin.Drawing;

namespace Savchin.Web.UI
{
    public class MessageBox : Window
    {

        HtmlTable table = new HtmlTable();
        HtmlTableCell cellMessage = new HtmlTableCell();
        HtmlTableRow buttonRow = new HtmlTableRow();
        HtmlTableCell cellTitle = new HtmlTableCell();
        HtmlTableCell cellButton = new HtmlTableCell();
        Label titleLable = new Label();
        Literal message = new Literal();

        public delegate void MessageBoxHandler(object sender, MessageBoxEventArgs e);
        public event MessageBoxHandler Result;

        #region Properties

        private DialogResult dialogResult = DialogResult.None;
        /// <summary>
        /// Gets the dialog result.
        /// </summary>
        /// <value>The dialog result.</value>
        public DialogResult DialogResult
        {
            get { return dialogResult; }
        }

        #region Behavior

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        public String CommandName
        {
            get
            {
                object value = ViewState["CommandName"];
                return ((value == null) ? String.Empty : (String)value);
            }

            set
            {
                ViewState["CommandName"] = value;
            }
        }

        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue("")]
        [Localizable(true)]
        public String CommandArgument
        {
            get
            {
                object value = ViewState["CommandArgument"];
                return ((value == null) ? String.Empty : (String)value);
            }

            set
            {
                ViewState["CommandArgument"] = value;
            }
        }
        #endregion

        #region Appearrance prop
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public MessageButtons Buttons
        {
            get
            {
                object value = ViewState["Buttons"];
                return ((value == null) ? MessageButtons.OK : (MessageButtons)value);
            }

            set
            {
                ViewState["Buttons"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("Message Title")]
        [Localizable(true)]
        public string Title
        {
            get
            {
                String s = (String)ViewState["Title"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Title"] = value;
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public String BodyCssClass
        {
            get
            {
                object value = ViewState["BodyCssClass"];
                return ((value == null) ? String.Empty : (String)value);
            }

            set
            {
                ViewState["BodyCssClass"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public String ButtonCellCssClass
        {
            get
            {
                object value = ViewState["ButtonCellCssClass"];
                return ((value == null) ? String.Empty : (String)value);
            }

            set
            {
                ViewState["ButtonCellCssClass"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public String TitleCssClass
        {
            get
            {
                object value = ViewState["TitleCssClass"];
                return ((value == null) ? String.Empty : (String)value);
            }

            set
            {
                ViewState["TitleCssClass"] = value;
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public String MessageCssClass
        {
            get
            {
                object value = ViewState["MessageCssClass"];
                return ((value == null) ? String.Empty : (String)value);
            }

            set
            {
                ViewState["MessageCssClass"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("Message Text")]
        [Localizable(true)]
        public string Text
        {
            get
            {

                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }
        #endregion

        #region Layout

        [Bindable(true)]
        [Category("Layout")]
        [DefaultValue("")]
        [Localizable(true)]
        public Unit Left
        {
            get
            {
                object value = ViewState["Left"];
                return ((value == null) ? Unit.Empty : (Unit)value);
            }

            set
            {
                ViewState["Left"] = value;
            }
        }

        [Bindable(true)]
        [Category("Layout")]
        [DefaultValue("")]
        [Localizable(true)]
        public Unit Top
        {
            get
            {
                object value = ViewState["Top"];
                return ((value == null) ? Unit.Empty : (Unit)value);
            }

            set
            {
                ViewState["Top"] = value;
            }
        }


        #endregion

        #endregion



        protected void OnResult(MessageBoxEventArgs e)
        {
            if (Result != null)
                Result(this, e);
        }


        #region Ctreate Controls

        protected override void CreateChildControls()
        {
            CreateMessageBoxTable();
            Controls.Add(table);
            ChildControlsCreated = true;
        }



        private void CreateMessageBoxTable()
        {


            if (BackColor != Color.Empty)
                table.BgColor = ConverterColor.ToHTMLColor(BackColor);

            table.CellPadding = 0;
            table.CellSpacing = 0;
            table.Border = 0;

            table.Width = "100%";
            table.Height = "100%";

            table.Rows.Add(CreateTitleRow());
            table.Rows.Add(CreateMessageRow());
            table.Rows.Add(CreateButtonRow());


        }
        private HtmlTableRow CreateTitleRow()
        {

            cellButton.Width = "15px";
            cellButton.Align = "center";

            LinkButton closeButton = new LinkButton();
            closeButton.Text = "x";
            closeButton.Style.Add("text-decoration", "none");
            closeButton.CommandArgument = DialogResult.Cancel.ToString();
            closeButton.Click += new EventHandler(CommandButton_Click);
            cellButton.Controls.Add(closeButton);

            cellTitle.Controls.Add(titleLable);




            HtmlTableRow result = new HtmlTableRow();
            result.Height = "20px";
            result.VAlign = "top";
            result.Cells.Add(cellTitle);
            result.Cells.Add(cellButton);

            return result;
        }
        private HtmlTableRow CreateMessageRow()
        {

            cellMessage.ColSpan = 2;


            cellMessage.Controls.Add(message);

            HtmlTableRow result = new HtmlTableRow();
            result.Cells.Add(cellMessage);
            return result;
        }

        private HtmlTableRow CreateButtonRow()
        {

            HtmlTableCell cell = new HtmlTableCell();
            cell.ColSpan = 2;


            switch (Buttons)
            {
                case MessageButtons.OK:
                    cell.Controls.Add(CreateButton("OK", DialogResult.OK));

                    break;
                case MessageButtons.OKCancel:
                    cell.Controls.Add(CreateButton("OK", DialogResult.OK));
                    cell.Controls.Add(GetSpace());
                    cell.Controls.Add(CreateButton("Cancel", DialogResult.Cancel));
                    break;
                case MessageButtons.YesNo:
                    cell.Controls.Add(CreateButton("Yes", DialogResult.Yes));
                    cell.Controls.Add(GetSpace());
                    cell.Controls.Add(CreateButton("No", DialogResult.No));
                    break;
                case MessageButtons.RetryCancel:
                    cell.Controls.Add(CreateButton("Retry", DialogResult.Retry));
                    cell.Controls.Add(GetSpace());
                    cell.Controls.Add(CreateButton("Cancel", DialogResult.Cancel));
                    break;
                case MessageButtons.AbortRetryIgnore:
                    cell.Controls.Add(CreateButton("Retry", DialogResult.Retry));
                    cell.Controls.Add(GetSpace());
                    cell.Controls.Add(CreateButton("Abort", DialogResult.Abort));
                    cell.Controls.Add(GetSpace());
                    cell.Controls.Add(CreateButton("Ignore", DialogResult.Ignore));
                    break;
                case MessageButtons.YesNoCancel:
                    cell.Controls.Add(CreateButton("Yes", DialogResult.Yes));
                    cell.Controls.Add(GetSpace());
                    cell.Controls.Add(CreateButton("No", DialogResult.No));
                    cell.Controls.Add(GetSpace());
                    cell.Controls.Add(CreateButton("Cancel", DialogResult.Cancel));
                    break;
            }



            buttonRow.Cells.Add(cell);
            return buttonRow;
        }

        private static Literal GetSpace()
        {
            Literal space = new Literal();
            space.Text = "&nbsp;";
            return space;
        }

        private Button CreateButton(string text, DialogResult result)
        {
            Button button = new Button();

            button.CommandArgument = result.ToString();
            button.Text = text;
            button.Click += new EventHandler(CommandButton_Click);
            return button;
        }

        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            EnsureChildControls();
            base.OnInit(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            Style.Add("position", "absolute");

            if (Left != Unit.Empty)
                Style.Add("left", Left.ToString());


            if (Top != Unit.Empty)
                Style.Add("top", Top.ToString());


            if (BodyCssClass != String.Empty)
                table.Attributes.Add("class", BodyCssClass);

            if (MessageCssClass != String.Empty)
                cellMessage.Attributes.Add("class", MessageCssClass);


            if (ButtonCellCssClass == string.Empty)
            {
                buttonRow.Height = "30px";
                buttonRow.VAlign = "top";
                buttonRow.Align = "center";
            }
            else
            {
                buttonRow.Attributes.Add("class", ButtonCellCssClass);
            }

            if (TitleCssClass != String.Empty)
            {
                cellTitle.Attributes.Add("class", TitleCssClass);
                cellButton.Attributes.Add("class", TitleCssClass);
            }

            titleLable.Text = Title;
            message.Text = Text;
            base.OnPreRender(e);

        }

        /// <summary>
        /// Handles the Click event of the CommandButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CommandButton_Click(Object sender, EventArgs e)
        {
            IButtonControl button = (IButtonControl)sender;
            dialogResult = (DialogResult)Enum.Parse(typeof(DialogResult), button.CommandArgument);
            OnResult(new MessageBoxEventArgs(dialogResult, CommandName, CommandArgument));
        }

        /// <summary>
        /// Shows the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="buttons">The buttons.</param>
        public static void Show(Page page, string title, string message,
                                MessageButtons buttons)
        {
            MessageBox box = new MessageBox();
            box.Title = title;
            box.Text = message;
            box.Buttons = buttons;
            foreach (Control control in page.Form.Controls)
            {
                if (control is ContentPlaceHolder)
                {
                    control.Controls.Add(box);
                    return;
                }
            }
        }

    }
}
