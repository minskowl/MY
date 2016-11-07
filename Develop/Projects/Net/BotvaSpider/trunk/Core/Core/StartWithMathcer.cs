using WatiN.Core.Comparers;
using WatiN.Core.Interfaces;

namespace BotvaSpider.Core
{
    internal class StartWithMathcer : Comparer<string>
    {

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }


        /// <summary>
        /// Compares the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool Compare(string value)
        {
            return string.IsNullOrEmpty(value) ? false : value.StartsWith(Text);
        }
    }
}