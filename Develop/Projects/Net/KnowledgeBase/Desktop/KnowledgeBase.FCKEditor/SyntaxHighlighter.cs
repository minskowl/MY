using System;
using System.IO;
using KnowledgeBase.FCKEditor;

namespace KnowledgeBase.Desktop.Controls
{
    class SyntaxHighlighter : HtmlEditorPlugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxHighlighter"/> class.
        /// </summary>
        public SyntaxHighlighter()
            : base("syntaxhighlight")
        {
        }

        public override string GetHeaderContent()
        {
            return string.Format(@"
<script type=""text/javascript"" src=""{0}/scripts/shCore.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushBash.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushCpp.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushCSharp.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushCss.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushDelphi.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushDiff.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushGroovy.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushJava.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushJScript.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushPhp.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushPlain.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushPython.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushRuby.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushScala.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushSql.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushVb.js""></script>
<script type=""text/javascript"" src=""{0}/scripts/shBrushXml.js""></script>
<link type=""text/css"" rel=""stylesheet"" href=""{0}/styles/shCore.css""/>
<link type=""text/css"" rel=""stylesheet"" href=""{0}/styles/shThemeDefault.css""/>
<script type=""text/javascript"">
  SyntaxHighlighter.config.clipboardSwf = '{0}/scripts/clipboard.swf';
  SyntaxHighlighter.all();
 </script>
", PluginPath);
        }


    }
}
