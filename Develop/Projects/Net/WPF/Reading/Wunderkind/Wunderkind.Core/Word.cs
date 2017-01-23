using System;
using System.Linq;

namespace Reading.Core
{
    public class Word
    {
        public string Syllabled { get; private set; }
        public int SyllablesCount { get; private set; }
        public string Text { get; private set; }
        public int Length { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Word"/> class.
        /// </summary>
        /// <param name="syllabled">The syllabled.</param>
        public Word(string syllabled)
        {
            Syllabled = syllabled;
            SyllablesCount = syllabled.ToCharArray().Count(c => c == '-') + 1;
            Text = syllabled.Replace("-", String.Empty);
            Length = Text.Length;
        }
    }
}