#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.IO;
using System.Text;

namespace Savchin.Web
{
    /// <summary>
    /// JavaScriptBuilder
    /// </summary>
    public class JavaScriptBuilder
    {

        readonly StringBuilder builder = new StringBuilder();
        private bool _withTags = false;
        /// <summary>
        /// Gets or sets a value indicating whether [with tags].
        /// </summary>
        /// <value><c>true</c> if [with tags]; otherwise, <c>false</c>.</value>
        public bool WithTags
        {
            get { return _withTags; }
            set { _withTags = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JavaScriptBuilder"/> class.
        /// </summary>
        public JavaScriptBuilder()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JavaScriptBuilder"/> class.
        /// </summary>
        /// <param name="withTags">if set to <c>true</c> [with tags].</param>
        public JavaScriptBuilder(bool withTags)
        {
            WithTags = withTags;
        }

        #region Appends
        /// <summary>
        /// Appends the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public StringBuilder Append(string value)
        {
            return builder.Append(value);
        }



        /// <summary>
        /// Appends the line.
        /// </summary>
        /// <param name="value">The value.</param>
        public StringBuilder AppendLine(string value)
        {
            return builder.AppendLine(value);
        }
        /// <summary>
        /// Appends the string constant.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public StringBuilder AppendStringConstant(string value)
        {
            return builder.Append("'" + ApplyEscapeSequences(value) + "'");
        }

        /// <summary>
        /// Appends the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public StringBuilder AppendFormat(string format, params object[] args)
        {
            return builder.AppendFormat(null, format, args);
        } 
        #endregion

        /// <summary>
        /// Gets the script.
        /// </summary>
        /// <returns></returns>
        public string GetScript()
        {
            if (_withTags)
                return
                    string.Format("<script type=\"text/javascript\" language=\"javascript\">\n{0}\n</script>", builder);
            else
                return builder.ToString();
        }

        /// <summary>
        /// Gets the javascript wrapped.
        /// </summary>
        /// <returns></returns>
        public string GetJavascriptWrapped()
        {
            return WrapHtmlWithJavaScript(GetScript());
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterPriority>2</filterPriority>
        public override string ToString()
        {
            return GetScript();
        }

        #region Statics

        /// <summary>
        /// Gets the append script include string.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <returns></returns>
        public static string GetJsScriptInclude(string src)
        {
            return String.Format("<script src='{0}' type='text/javascript'  language='javascript' ></script>", src);
        }
        /// <summary>
        /// Gets the CSS include.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <returns></returns>
        public static string GetCssInclude(string src)
        {
            return String.Format("<link href='{0}' type='text/css' rel='stylesheet' />", src);
        }

        /// <summary>
        /// Converts html code to java script than insert this conde
        /// inside client html page
        /// </summary>
        /// <param name="htmlCode">Html code to convert</param>
        /// <returns>Body of JavaScript</returns>
        public static string WrapHtmlWithJavaScript(string htmlCode)
        {
            if (string.IsNullOrEmpty(htmlCode)) return htmlCode;

            StringBuilder document = new StringBuilder();
            using (StringReader reader = new StringReader(htmlCode))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (!string.IsNullOrEmpty(line))
                        document.AppendFormat("document.write(\"{0}\");\n", ApplyEscapeSequences(line));
                    line = reader.ReadLine();
                }
            }
            return document.ToString();
        }


        /// <summary>
        /// Converts html code into java script line
        /// </summary>
        /// <param name="htmlCode">Html code to convert</param>
        /// <returns>Converted line</returns>
        public static string ConvertToJavaScriptLine(string htmlCode)
        {
            StringBuilder document = new StringBuilder();

            foreach (string line in htmlCode.Split('\r', '\n'))
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                document.Append(ApplyEscapeSequences(line));
            }

            return "'" + document + "'";
        }


        /// <summary>
        /// Applies java script escate sequenses
        /// </summary>
        /// <param name="htmlLine">Html code to convert</param>
        /// <returns>Escaped sequense</returns>
        public static string ApplyEscapeSequences(string htmlLine)
        {
            return htmlLine.Replace(@"\", @"\\").Replace(@"'", @"\'").Replace("\"", "\\" + "\"").Replace(Environment.NewLine, "\\n").Replace("\n", "\\n");
        }


        #endregion
    }
}
