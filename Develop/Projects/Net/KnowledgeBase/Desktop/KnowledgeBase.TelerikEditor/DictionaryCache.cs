using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using KnowledgeBase.Core;
using Telerik.Windows.Documents.Proofing;

namespace KnowledgeBase.TelerikEditor
{
    static class DictionaryCache
    {
        internal static readonly Dictionary<CultureInfo, RadDictionary> Dictionaries = new Dictionary<CultureInfo, RadDictionary>();

        /// <summary>
        /// Initializes the <see cref="DictionaryCache"/> class.
        /// </summary>
        static DictionaryCache()
        {
            Load("en-US");
            Load("ru-RU");

        }

        /// <summary>
        /// Sets the dictionaries.
        /// </summary>
        /// <param name="checker">The checker.</param>
        internal static void SetDictionaries(DocumentSpellChecker checker)
        {
            foreach (var dic in Dictionaries)
            {
                try
                {
                    checker.AddDictionary(dic.Value, dic.Key);
                }
                catch (Exception ex)
                {
                    AppCore.Log.ErrorFormat("Error add  dictionary {1} \n {0}", ex, dic.Key.Name);
                }
            }


        }

        private static void Load(string culture)
        {
            var fileName = string.Format("Resources\\{0}.tdf", culture);
            try
            {
                using (var stream = File.Open(fileName, FileMode.Open))
                {
                    var res = new RadDictionary();
                    res.Load(stream);
                    Dictionaries.Add(new CultureInfo(culture), res);

                }
            }
            catch (Exception ex)
            {
                AppCore.Log.ErrorFormat("Error read dictionary {0} \n {1}", fileName, ex);
            }
        }

    }
}
