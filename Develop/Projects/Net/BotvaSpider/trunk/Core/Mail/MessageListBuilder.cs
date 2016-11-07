using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BotvaSpider.Core
{
    class MessageListBuilder : BuilderBase
    {
        private readonly Regex statReg = new Regex(@"(<table\swidth=""504""\sborder=""0""\scellpadding=""0""\scellspacing=""0""\sclass=""text_main_2"">)(?<content>.*?)(</table>)",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        private readonly Regex messageReg = new Regex(@"<a\shref='(?<url>[^'""]+)'\s+class='(?<class>[^'""]+)'>(?<text>[^<>]+)</a>",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled); 
        /// <summary>
        /// Builds the specified HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        public List<MessageInfo> Build(string html)
        {
            var content = statReg.Match(html).Groups["content"].Captures[0].Value;
            var rows = rowReg.Matches(content);
            var result = new List<MessageInfo>();
            foreach (Match row in rows)
            {
                var cells = cellReg.Matches(row.Value);
                var date = cells[1].Groups["content"].Value;
                var message =messageReg.Match( cells[2].Groups["content"].Value);
                result.Add(new MessageInfo
                {
                    Date = DateTime.Parse(date),
                    Title = message.Groups["text"].Value
                    
                });
            }
            return result;
        }
    }
}
