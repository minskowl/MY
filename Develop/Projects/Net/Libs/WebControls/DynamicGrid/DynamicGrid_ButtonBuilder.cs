#region Version & Copyright
/* 
 * $Id: DynamicGrid_ButtonBuilder.cs 26757 2008-01-16 15:49:41Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Savchin.Web;


namespace Savchin.Web.UI
{

    /// <summary>
    /// Generates dynamic grid button html code
    /// </summary>
    public partial class DynamicGrid : ICallbackEventHandler
    {
        #region Constants


        private const string ViewAltText = "View";
        private const string EditAltText = "Edit";
        private const string DeleteAltText = "Delete";


        #endregion

        #region Properties

        private PopupWindowJavaScriptBuilder editPopup= new PopupWindowJavaScriptBuilder("showEdit");
        /// <summary>
        /// Gets or sets the edit popup.
        /// </summary>
        /// <value>The edit popup.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public PopupWindowJavaScriptBuilder EditPopup
        {
            get { return editPopup; }
            set { editPopup = value; }
        }
        private PopupWindowJavaScriptBuilder viewPopup = new PopupWindowJavaScriptBuilder("showView");
        /// <summary>
        /// Gets or sets the edit popup.
        /// </summary>
        /// <value>The edit popup.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public PopupWindowJavaScriptBuilder ViewPopup
        {
            get { return viewPopup; }
            set { viewPopup = value; }
        }
        private string _deleteCallback
        {
            get { return ClientID + "deleteCallback"; }
        }

        private string _deleteButtonHandler
        {
            get { return ClientID + "deleteButtonHandler"; }

        }
        private string _commandCallback
        {
            get { return ClientID + "dtmlXMLCommandCallback"; }
        }
        private string _commandButtonHandler
        {
            get { return ClientID + "commandButtonHandler"; }
        }


        #endregion

        /// <summary>
        /// Gets the command handler.
        /// </summary>
        /// <param name="refreshGrid">if set to <c>true</c> [refresh grid].</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public string GetCommandHandler(bool refreshGrid, string command)
        {
            RegisterCommandCallback();
            string handler;
            if (refreshGrid)
                handler = _commandButtonHandler + "('" + command + "',{0},1);";
            else
                handler = _commandButtonHandler + "('" + command + "',{0},0);";
            return handler;
        }

        #region Buttons methods

        /// <summary>
        /// Gets the button HTML.
        /// </summary>
        /// <param name="href">The href.</param>
        /// <param name="useNewWindow">if set to <c>true</c> [use new window].</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="alternateText">Alternate text of image</param>
        /// <returns></returns>
        private string GetButtonHtml(string href, bool useNewWindow, string imageUrl, string alternateText)
        {
            DynamicGridButton button = new DynamicGridButton();
            button.NavigateUrl = JavaScriptBuilder.ConvertToJavaScriptLine(href);
            if (useNewWindow)
                button.Target = "_blank";
            if (!string.IsNullOrEmpty(imageUrl))
                button.ImageUrl = ControlHelper.GetThemebleUrl(imageUrl, Page.Theme);
            button.AlternateText = alternateText;

            return button.GetHTML();

        }

        /// <summary>
        /// Gets the on click button HTML.
        /// </summary>
        /// <param name="handlerCode">The hander code.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="text">The button text.</param>
        /// <param name="alternateText">Alternate image text</param>
        /// <returns></returns>
        public string GetOnClickButtonHtml(string handlerCode, string imageUrl, string text, string alternateText)
        {
            DynamicGridButton button = new DynamicGridButton();
            if (!string.IsNullOrEmpty(imageUrl))
            {
                button.ImageUrl = ControlHelper.GetFullImageUrl(imageUrl, Page);
            }

            button.OnClientClick = handlerCode;
            button.Text = text;
            button.AlternateText = alternateText;
            return button.GetHTML();
        }
        /// <summary>
        /// Gets the commad button HTML.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="text">The text.</param>
        /// <param name="refreshGrid">if set to <c>true</c> [refresh grid].</param>
        /// <returns></returns>
        public string GetCommadButtonHtml(string command, string imageUrl, string text, bool refreshGrid)
        {
            return GetOnClickButtonHtml(GetCommandHandler(refreshGrid, command),
                                        imageUrl,
                                        text,
                                        string.Empty);
        }



        #region View Button

        /// <summary>
        /// Gets the view button HTML.
        /// </summary>
        /// <param name="href">The href.</param>
        /// <param name="useNewWindow">if set to <c>true</c> [use new window].</param>
        /// <returns></returns>
        public string GetViewButtonHtml(string href, bool useNewWindow)
        {
            return GetButtonHtml(href, useNewWindow, ImagePathProvider.PreviewImage, ViewAltText);
        }


        /// <summary>
        /// Gets the view button HTML.
        /// </summary>
        /// <param name="onclickHandler">The onclick handler.</param>
        /// <returns></returns>
        public string GetViewButtonHtml(string onclickHandler)
        {
            return GetOnClickButtonHtml(onclickHandler, ImagePathProvider.PreviewImage, string.Empty, ViewAltText);
        }

        /// <summary>
        /// Gets the view popup button HTML.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public string GetViewPopupButtonHtml(string url)
        {
            needRegisterViewPopupScript = true;
            if (!viewPopup.FunctionName.EndsWith(ClientID))
                viewPopup.FunctionName = viewPopup.FunctionName + ClientID;

            return GetViewButtonHtml(viewPopup.FunctionName + "('" + url + "');");
        }
        #endregion

        #region Delete Button
        /// <summary>
        /// Gets the delete button HTML.
        /// </summary>
        /// <returns></returns>
        public string GetDeleteButtonHtml()
        {
            RegisterDeleteCallback();
            return GetOnClickButtonHtml(_deleteButtonHandler + "({0});", ImagePathProvider.DeleteImage, string.Empty, DeleteAltText);
        }
        /// <summary>
        /// Gets the delete button HTML.
        /// </summary>
        /// <param name="onclickHandler">The onclick handler.</param>
        /// <returns></returns>
        public string GetDeleteButtonHtml(string onclickHandler)
        {
            return GetOnClickButtonHtml(onclickHandler, ImagePathProvider.DeleteImage, string.Empty, DeleteAltText);
        }


        #endregion


        #region Edit Button
        /// <summary>
        /// Gets the edit button HTML.
        /// </summary>
        /// <param name="onclickHandler">The onclick handler.</param>
        /// <returns></returns>
        public string GetEditButtonHtml(string onclickHandler)
        {
            return GetOnClickButtonHtml(onclickHandler, ImagePathProvider.EditImage, string.Empty, EditAltText);
        }
        /// <summary>
        /// Gets the edit popup button HTML.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public string GetEditPopupButtonHtml(string url)
        {
            needRegisterEditPopupScript = true;
            if (!editPopup.FunctionName.EndsWith(ClientID))
                editPopup.FunctionName = editPopup.FunctionName + ClientID;

            return GetEditButtonHtml(editPopup.FunctionName + "('" + url + "');");
        }


        #endregion




        #endregion

        #region Events


        private static readonly object EventDeleteCommand = new object();
        private static readonly object EventCommand = new object();
        /// <summary>
        /// Occurs when 
        /// </summary>
        [Category("Action")]
        public event DeleteCommandEventHandler DeleteCommand
        {
            add
            {
                Events.AddHandler(EventDeleteCommand, value);
            }
            remove
            {
                Events.RemoveHandler(EventDeleteCommand, value);
            }
        }
        /// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.Button"></see> control is clicked.</summary>
        [Category("Action")]
        public event GridCommandEventHandler Command
        {
            add
            {
                Events.AddHandler(EventCommand, value);
            }
            remove
            {
                Events.RemoveHandler(EventCommand, value);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:Command"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Janus.Web.UI.CommandBars.CommandEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDeleteCommand(DeleteCommandEventArgs e)
        {
            DeleteCommandEventHandler handler1 = (DeleteCommandEventHandler)Events[EventDeleteCommand];
            if (handler1 != null)
            {
                handler1(this, e);
                callbackResult = (e.IsSuccessful) ? e.CommandArgument : "error";
            }
            RaiseBubbleEvent(this, e);

        }
        /// <summary>
        /// Raises the <see cref="E:Command"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCommand(GridCommandEventArgs e)
        {
            GridCommandEventHandler handler1 = (GridCommandEventHandler)Events[EventCommand];
            if (handler1 != null)
            {
                handler1(this, e);
                if (e.IsProcessed)
                    callbackResult = string.Format("{0}|{1}|{2}",
                                                   (e.IsSuccessful) ? "ok" : "error",
                                                   e.CommandArgument,
                                                   e.Message);


            }
            RaiseBubbleEvent(this, e);

        }
        #endregion
        private bool needRegisterViewPopupScript = false;
        private bool needRegisterEditPopupScript = false;
        private void RegisterPopupScript()
        {
            if (needRegisterViewPopupScript)
                viewPopup.RegisterClientScriptBlock(Page, GetType());

            if (needRegisterEditPopupScript)
                editPopup.RegisterClientScriptBlock(Page, GetType());
        }




        private bool commanCallbackRegistered = false;
        /// <summary>
        /// Registers the command callback.
        /// </summary>
        private void RegisterCommandCallback()
        {
            if (commanCallbackRegistered)
                return;

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("var " + ClientID + "refresh=0;");
            builder.AppendLine("function " + _commandCallback + "(result)");
            builder.AppendLine("{");
            builder.AppendLine("var args=result.split('|');");
            builder.AppendLine("if (args.length==3)");
            builder.AppendLine("{");
            builder.AppendLine("  alert(args[2]);");
            builder.AppendLine("  if(" + ClientID + "refresh > 0)");
            builder.AppendLine("  " + GetGridReloadScript());
            builder.AppendLine("}");
            builder.AppendLine("else if(args.length==2)");
            builder.AppendLine("{");
            builder.AppendLine("if (args[0] == 'error')");
            builder.AppendLine("alert('Error accured duiring operation');");
            builder.AppendLine("}");
            builder.AppendLine("}");

            builder.AppendLine("function " + _commandButtonHandler + "(command,rowId,refresh)");
            builder.AppendLine("{");
            builder.AppendLine("var cmd=command + '|'+rowId;");
            builder.AppendLine(Page.ClientScript.GetCallbackEventReference(
                this,
                "cmd",
                _commandCallback,
                null));
            builder.AppendLine("" + ClientID + "refresh=parseInt(refresh);");

            builder.AppendLine("}");


            Page.ClientScript.RegisterClientScriptBlock(GetType(), _commandButtonHandler, builder.ToString(), true);
            commanCallbackRegistered = true;
        }

        /// <summary>
        /// Registers the delete callback.
        /// </summary>
        private void RegisterDeleteCallback()
        {
            string callback = Page.ClientScript.GetCallbackEventReference(
                                            this,
                                            "cmd",
                                            _deleteCallback,
                                            null);
            string script = string.Format(@"function {0}(rowId)
{{
	if (rowId != 'error')
	{{
		{2}.deleteRow(rowId)
		alert('Object has been successfully deleted');
	}}
	else
		alert('Error has accured during removal');
}}

function {1}(rowId)
{{
if (confirm('Are you sure you want to delete the selected object?'))
	{{
		var cmd='delete|'+rowId;
	  {3};
	}}
}}
            ",
                                          _deleteCallback,
                                          _deleteButtonHandler,
                                          JsObjName,
                                          callback);

            Page.ClientScript.RegisterClientScriptBlock(GetType(),
                                                        _deleteButtonHandler,
                                                        script,
                                                        true);
        }

        private string callbackResult;
        /// <summary>
        /// Gets the callback result.
        /// </summary>
        /// <returns></returns>
        string ICallbackEventHandler.GetCallbackResult()
        {
            return callbackResult;
        }

        /// <summary>
        /// Processes a callback event that targets a control.
        /// </summary>
        /// <param name="eventArgument">A string that represents an event argument to pass to the event handler.</param>
        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            string[] arguments = eventArgument.Split(new char[] { '|' });
            if (arguments[0].CompareTo("delete") == 0)
            {
                DeleteCommandEventArgs deleteEventArgs = new DeleteCommandEventArgs(arguments[1]);
                OnDeleteCommand(deleteEventArgs);
            }
            GridCommandEventArgs commandEventArgs = new GridCommandEventArgs(arguments[0], arguments[1]);
            OnCommand(commandEventArgs);

        }



    }
}
