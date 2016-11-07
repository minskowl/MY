using System;
using System.Text;
using System.Web.UI;

namespace Savchin.Web
{
    /// <summary>
    /// Summary description for PopupWindowJavaScriptBuilder
    /// </summary>
    public class PopupWindowJavaScriptBuilder
    {
        /// <summary>
        /// Default Popup Function. Showed popup width 600 height 400
        /// </summary>
        public const string DefaultPopupFunction = "showPopup";

        /// <summary>
        /// ResizablOptions
        /// </summary>
        public const string ResizableOptions = "resizable=1,status=1,scrollbars=1";

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private bool useMaxHeight = false;

        private int width = 600;
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        private int height = 400;
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private string options;
        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        public string Options
        {
            get { return options; }
            set { options = value; }
        }

        private bool _addJsPrefix = true;

        /// <summary>
        /// Specifies if javaScript prefix should be added for script
        /// </summary>
        public bool AddJsPrefix
        {
            get { return _addJsPrefix; }
            set { _addJsPrefix = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use max height].
        /// </summary>
        /// <value><c>true</c> if [use max height]; otherwise, <c>false</c>.</value>
        public bool UseMaxHeight
        {
            get { return useMaxHeight; }
            set { useMaxHeight = value; }
        }

        /// <summary>
        /// Gets or sets the name of the function.
        /// </summary>
        /// <value>The name of the function.</value>
        public string FunctionName
        {
            get { return functionName; }
            set { functionName = value; }
        }

        private string functionName = DefaultPopupFunction;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupWindowJavaScriptBuilder"/> class.
        /// </summary>
        public PopupWindowJavaScriptBuilder()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PopupWindowJavaScriptBuilder"/> class.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        public PopupWindowJavaScriptBuilder(string functionName)
        {
            this.functionName = functionName;
        }

        /// <summary>
        /// Gets the script.
        /// </summary>
        /// <param name="pageUrl">The page quoted URL.</param>
        /// <returns></returns>
        private string GetScript(string pageUrl)
        {
            string prefix = "javascript:";

            if (!AddJsPrefix)
                prefix = string.Empty;

            string hString = (UseMaxHeight) ? "'+ window.screen.availHeight +'" : Height.ToString();
            string tString = (UseMaxHeight) ? "0" : "' + (window.screen.availHeight / 2 - " + Height + " / 2)+ '";
            return string.Format("{0}window.open({1},'','width={2}, height={3}, " +
                        " left=' + (window.screen.availWidth / 2 - {2} / 2) + " +
                        "', top={4}{5}')",
                        prefix, pageUrl, Width, hString, tString,
                    (string.IsNullOrEmpty(options) ? string.Empty : ", " + options)
                    );
        }

        /// <summary>
        /// Generates java script for page displaying
        /// </summary>
        /// <param name="pageUrl">Url or script to display</param>
        /// <param name="addQuotes">If flag is set page url will surrounded by quotes</param>
        /// <returns>Generated java script</returns>
        public string GetScript(string pageUrl, bool addQuotes)
        {
            if (addQuotes)
                return GetScript("'" + pageUrl + "'");
            return GetScript(pageUrl);
        }

        /// <summary>
        /// Get JavaScript to display preview window
        /// </summary>
        /// <param name="linkToDisplay">Link to display inside poput window</param>
        /// <param name="addQuotes">if set to <c>true</c> [add quotes].</param>
        /// <param name="addJsPrefix">if set to <c>true</c> [add js prefix].</param>
        /// <returns>Java script used to create popup window</returns>
        public static string GetPreviewJavaScript(string linkToDisplay, bool addQuotes,
            bool addJsPrefix)
        {
            return GetPreviewJavaScript(linkToDisplay, addQuotes, addQuotes, 600, 400);
        }

        /// <summary>
        /// Gets the preview java script.
        /// </summary>
        /// <param name="linkToDisplay">The link to display.</param>
        /// <param name="addQuotes">if set to <c>true</c> [add quotes].</param>
        /// <param name="addJsPrefix">if set to <c>true</c> [add js prefix].</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static string GetPreviewJavaScript(string linkToDisplay, bool addQuotes,
          bool addJsPrefix, int width, int height)
        {
            PopupWindowJavaScriptBuilder builder = new PopupWindowJavaScriptBuilder();

            builder.AddJsPrefix = addJsPrefix;
            builder.Width = width;
            builder.Height = height;

            builder.Options = ResizableOptions;

            return builder.GetScript(linkToDisplay, addQuotes);
        }

        /// <summary>
        /// Get JavaScript to display preview window
        /// </summary>
        /// <param name="linkToDisplay">Link to display inside poput window</param>
        /// <param name="addQuotes">if set to <c>true</c> [add quotes].</param>
        /// <returns>Java script used to create popup window</returns>
        public static string GetPreviewJavaScript(string linkToDisplay, bool addQuotes)
        {
            return GetPreviewJavaScript(linkToDisplay, addQuotes, true);
        }

        /// <summary>
        /// Get JavaScript to display preview window
        /// </summary>
        /// <param name="linkToDisplay">Link to display inside poput window</param>
        /// <returns>Java script used to create popup window</returns> 
        public string GetPreviewJavaScript(string linkToDisplay)
        {
            return GetPreviewJavaScript(linkToDisplay, true);
        }

        /// <summary>
        /// Registers the client script block.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="type">The type.</param>
        public void RegisterClientScriptBlock(Page page, Type type)
        {
            page.ClientScript.RegisterClientScriptBlock(
                type,
                FunctionName,
                GetPopupJavaScriptFunction(),
                true);
        }

        /// <summary>
        /// Gets the popup java script function.
        /// </summary>
        /// <returns></returns>
        public string GetPopupJavaScriptFunction()
        {
            StringBuilder builder = new StringBuilder();
            AddJsPrefix = false;
            builder.AppendLine("function " + FunctionName + "(url)");
            builder.AppendLine("{");
            builder.AppendLine(GetScript("url"));
            builder.AppendLine("}");

            return builder.ToString();
        }
    }
}
