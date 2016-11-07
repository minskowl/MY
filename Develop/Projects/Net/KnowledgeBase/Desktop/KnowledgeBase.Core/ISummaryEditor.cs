using System.Collections.Generic;
using System.Windows;
using Savchin.Core;

namespace KnowledgeBase.Core
{
    public interface ISummaryEditor
    {
        IEnumerable<NameValuePair<string>> UserFiles {  set; }
        IEnumerable<NameValuePair<string>> ArticleFiles {  set; }

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <value>The HTML.</value>
        string Value { get; set; }


    }
}