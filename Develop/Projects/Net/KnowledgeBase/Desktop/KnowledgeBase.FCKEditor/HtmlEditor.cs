using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Forms;
using KnowledgeBase.Core;
using Savchin.Core;
using Savchin.Wpf.Core;

namespace KnowledgeBase.FCKEditor
{
    public partial class HtmlEditor : WindowsFormsHost, ISummaryEditor
    {
        public const string Folder = "ckeditor";
        private WebBrowser _htmlEditor;


        public IEnumerable<NameValuePair<string>> UserFiles { get; set; }
        public IEnumerable<NameValuePair<string>> ArticleFiles { get; set; }




        private string _html;
        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <value>The HTML.</value>
        public string Value
        {
            get
            {
                var res = (string)InvokeScript("getHtml") ?? string.Empty;
                return res.Trim().Replace(AppCore.Settings.ContentPath, AppCore.Settings.ContentFolder + "\\");
            }
            set { _html = value; }
        }

        public static string EditorPath = AppCore.Settings.HtmlEditorPath;



        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlEditor"/> class.
        /// </summary>
        public HtmlEditor()
        {
            if (this.IsDesignMode()) return;

            _htmlEditor = new WebBrowser();
            _htmlEditor.DocumentCompleted += HtmlEditorDocumentCompleted;

            Child = _htmlEditor;
            _htmlEditor.Navigate(Path.Combine(EditorPath, "index.html"));
        }

        #region Interface



        #endregion
        void HtmlEditorDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            InvokeScript("setHtml", _html ?? string.Empty);
            InvokeScript("setFiles", new FileBrowser(UserFiles, ArticleFiles).BuildXml());
        }

        #region Javascript Call
        private object InvokeScript(string function, object[] args)
        {
            return _htmlEditor == null || _htmlEditor.Document == null ? null
                       : _htmlEditor.Document.InvokeScript(function, args);
        }

        private object InvokeScript(string function, object arg)
        {
            return _htmlEditor == null || _htmlEditor.Document == null ? null
                       : _htmlEditor.Document.InvokeScript(function, new object[1] { arg });
        }

        private object InvokeScript(string function)
        {
            return _htmlEditor == null || _htmlEditor.Document == null ? null
                       : _htmlEditor.Document.InvokeScript(function);
        }
        #endregion
    }
}
