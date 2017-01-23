using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Savchin.Core;
using Wunderkind.Core;

namespace Reading.Core
{
    public class WordsCollection
    {

        private Word[] _words;
        private int[] _sylablesCount;

        public WordsCollection()
        {
            Load();
        }

        /// <summary>
        /// Loads the specified folder.
        /// </summary>
        private void Load()
        {
            try
            {
                LoadFile(ResourceProvider.WordFile);
            }
            catch (Exception ex)
            {
                ReadingContext.Current.Logger.Warning( "Error load file ", ex);
            }
        }

        /// <summary>
        /// Loads the file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void LoadFile(string path)
        {

            _words = File.ReadAllLines(path)
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .Select(e => new Word(e)).ToArray();

            _sylablesCount = _words.Select(e => e.SyllablesCount).Distinct().OrderBy(e => e).ToArray();
        }

        /// <summary>
        /// Gets the syllable counts.
        /// </summary>
        /// <returns></returns>
        public int[] GetSyllableCounts()
        {
            return _sylablesCount;
        }



        /// <summary>
        /// Gets the words.
        /// </summary>
        /// <param name="sylablesCount">The sylables count.</param>
        /// <param name="wordLength">Length of the word.</param>
        /// <returns></returns>
        public List<Word> GetWords(Range<int> sylablesCount, Range<int> wordLength)
        {
            return _words.Where(e => sylablesCount.IsInRange(e.SyllablesCount) && wordLength.IsInRange(e.Length)).ToList();
        }

        /// <summary>
        /// Gets the words.
        /// </summary>
        /// <param name="wordLength">Length of the word.</param>
        /// <returns></returns>
        public List<Word> GetWords(Range<int> wordLength)
        {
            return _words.Where(e => wordLength.IsInRange(e.Length)).ToList();
        }
    }
}
