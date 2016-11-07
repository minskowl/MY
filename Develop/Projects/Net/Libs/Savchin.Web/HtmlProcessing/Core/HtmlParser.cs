using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Savchin.Web.HtmlProcessing.Core
{
    class HtmlParser
    {
        private const string TagGroupName = "tagName";
        private const string InnerContentGroupName = "innerContentGroup";
        private const string ClosingTagSymbol = "closingTag";
        private const string AttributeGroupName = "attrName";
        private const string DelimeterGroupName = "delimenter";
        private const string ValueGroupName = "value";
        private const string BaseUrlGroupName = "BaseUrl";
        private const string BaseFolderPattern = "<base[^>]+?href\\s*=\\s*[ '\"](?<" + BaseUrlGroupName + ">[^ '\">]+)[ '\"]";

        private const string AttributesPattern =
            @"(\s*(?<attrName>[\w-_]+)\s*=\s*(?<delimenter>"")(?<value>[^""]*))("")"
            + @"|(\s*(?<attrName>[\w-_]+)\s*=\s*(?<delimenter>')(?<value>[^']*))(')"
            + @"|(\s*(?<attrName>[\w-_]+)\s*=\s*(?<delimenter>\\"")(?<value>[^\\""]*))(\\"")"
            + @"|(\s*(?<attrName>[\w-_]+)\s*=\s*(?<delimenter>\\')(?<value>[^\\']*))(\\')"
            + @"|(\s*(?<attrName>[\w-_]+)\s*=\s*(?<value>[^ ]*))";
        private const string TagPattern = @"<(?<tagName>\w+)(?<innerContentGroup>[^>]*)(?<closingTag>/?>)";
        private readonly Regex regExpAttributes = new Regex(AttributesPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public readonly Regex RegExpTag = new Regex(TagPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Builds the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public TagInfo BuildTag(Match match)
        {
            var tag = new TagInfo
                          {
                              TagName = match.Groups[TagGroupName].Value,
                              ClosingSymbol = match.Groups[ClosingTagSymbol].Value
                          };

            FillTagAttributes(tag, match.Groups[InnerContentGroupName].Value);
            return tag;
        }

        /// <summary>
        /// Fills the tag attributes.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="tagContent">Content of the tag.</param>
        private void FillTagAttributes(TagInfo tag, string tagContent)
        {
            if (string.IsNullOrEmpty(tagContent))
                return;


            foreach (Match match in regExpAttributes.Matches(tagContent))
            {
                var info = new AttributeValueInfo
                                              {
                                                  Attribute = match.Groups[AttributeGroupName].Value,
                                                  Delimeter = match.Groups[DelimeterGroupName].Value,
                                                  Value = match.Groups[ValueGroupName].Value
                                              };

                tag.Attributes.Add(info);
            }
        }

        /// <summary>
        /// Gets the base URL folder.
        /// </summary>
        /// <param name="HtmlFile">The HTML file.</param>
        /// <returns></returns>
        public string GetBaseUrlFolder(IWebFile HtmlFile)
        {
            string baseUrlFolder = null;
            Match match = Regex.Match(HtmlFile.Text, BaseFolderPattern, RegexOptions.IgnoreCase);

            if (match.Success)
                baseUrlFolder = match.Groups[BaseUrlGroupName].Value;

            if (string.IsNullOrEmpty(baseUrlFolder))
            {
                var builder = new StringBuilder();
                builder.Append(HtmlFile.Url.GetLeftPart(UriPartial.Authority));

                for (int i = 0; i < HtmlFile.Url.Segments.Length - 1; i++)
                    builder.Append(HtmlFile.Url.Segments[i]);

                baseUrlFolder = builder.ToString();
            }

            if (!baseUrlFolder.EndsWith("/"))
                baseUrlFolder += "/";
            return baseUrlFolder;
        }
    }
}
