using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Savchin.Logging;

namespace Reading.Core
{
   public class SenetenceCollection
    {
        private string[] _words;


        public SenetenceCollection()
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
                LoadFile(ResourceProvider.SentencesFile);
            }
            catch (Exception ex)
            {
                ReadingContext.Current.Logger.AddMessage(Severity.Warning, "Error load file ", ex);
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
                .Select(e =>e.Trim())
                .ToArray();


        }

        /// <summary>
        /// Gets the sentences.
        /// </summary>
        /// <returns></returns>
        public List<string> GetSentences()
        {
            return _words.ToList();
        }
    }
}
