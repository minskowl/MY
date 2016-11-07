using System.Collections.Generic;
using System.Text;
using KnowledgeBase.Core;
using Savchin.Core;

namespace KnowledgeBase.FCKEditor
{
    public partial class HtmlEditor : ISummaryEditor
    {
        /// <summary>
        /// FileBrowser
        /// </summary>
        private class FileBrowser
        {
            private const string BaseImagePath = "treeview/images/";
            private readonly StringBuilder _builder;
            private IEnumerable<NameValuePair<string>> _userFiles;
            private IEnumerable<NameValuePair<string>> _artFiles;
            /// <summary>
            /// Initializes a new instance of the <see cref="FileBrowser"/> class.
            /// </summary>
            /// <param name="userFiles">The user files.</param>
            /// <param name="artFiles">The art files.</param>
            public FileBrowser( IEnumerable<NameValuePair<string>> userFiles, IEnumerable<NameValuePair<string>> artFiles)
            {
                _userFiles = userFiles;
                _artFiles = artFiles;

                _builder = new StringBuilder(@"<Tree CSSName = ""windows7"" EditLabel = ""false"" 
ShowLines = ""false"" LineStyle = ""default"" Visible = ""true"" PathSeparator = ""\"" 
ToggleOnSelect = ""false"" SelectedValuePath = """">");
            }

            /// <summary>
            /// Builds the XML.
            /// </summary>
            /// <returns></returns>
            public string BuildXml()
            {
                AddFiles(_artFiles,"Article Files");
                AddFiles(_userFiles,"User Files");
                _builder.AppendLine("</Tree>");
                return _builder.ToString();
            }


            private void AddFiles(IEnumerable<NameValuePair<string>> files ,string title )
            {
                AddFolderNode(title);
                foreach (var file in files)
                {
                    AddFileNode(file.Name, file.Value);
                }

               _builder.Append("</TreeNode>");
            }
            private void AddFileNode(string fileName, string filePath)
            {
                _builder.AppendFormat(
                    @"<TreeNode Text=""{0}"" Value=""{0}"" AllowKeyNavigation = ""true"" NavigationURL = ""{2}""
Target = ""viewer"" CollapsedImageURL = ""{1}file.png"" ExpandedImageURL = ""{1}file.png"" 
IsSelected = ""false"" IsExpanded = ""false"" WordWrap = ""true""/>", fileName, BaseImagePath, filePath);
            }

            private void AddFolderNode(string name)
            {
                _builder.AppendFormat(
                    @"<TreeNode Text=""{0}"" Value=""{0}"" 
AllowKeyNavigation = ""true"" CollapsedImageURL = ""{1}folder-closed.png"" ExpandedImageURL = ""{1}folder.png"" 
IsSelected = ""false"" IsExpanded = ""false"" Visible = ""true""
WordWrap = ""true"">", name, BaseImagePath);
            }


        }
    }
}
