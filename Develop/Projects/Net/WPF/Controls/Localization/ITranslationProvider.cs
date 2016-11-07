using System.Collections.Generic;
using System.Globalization;

namespace Savchin.Wpf.Controls.Localization
{
    public interface ITranslationProvider
    {


        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object Translate(string key);

        string TranslateString(string key);

    }
}
