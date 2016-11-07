using System.Text.RegularExpressions;

namespace BotvaSpider
{
    internal class BuilderBase
    {
        protected readonly Regex rowReg = new Regex(@"(<?<tr>)(.*?)</tr>",
                                                    RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline);

        protected readonly Regex cellReg = new Regex(@"(<?<td)([^>]*)?>(?<content>.*?)</td>", RegexOptions.Compiled | RegexOptions.Singleline);
        protected readonly Regex valueReg = new Regex(@"(<span[^>]*>)(.*?)(</span>)", RegexOptions.Compiled);
    }
}