using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;
using Savchin.Core;

namespace Savchin.Wpf.Controls.Localization
{
    public interface ITranslationManager
    {
        CultureInfo CurrentLanguage { get; set; }
        event EventHandler LanguageChanged;

        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Translate(string key);

        /// <summary>
        /// Translates the specified enums.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enums">The enums.</param>
        /// <returns></returns>
        IEnumerable<NameValuePair> Translate<T>(IEnumerable<T> enums);

        IEnumerable<NameValuePair> Translate<T>();
    }

    public class TranslationManager : ITranslationManager
    {



        #region Properties


        public CultureInfo CurrentLanguage
        {
            get { return CultureInfo.CurrentUICulture; }
            set

            {

                if (value == CultureInfo.CurrentUICulture) return;
           //     Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride

             //   CultureInfo.CurrentUICulture = value;
                OnLanguageChanged();
            }
        }

        public ITranslationProvider TranslationProvider { get; set; }

        public static TranslationManager Instance { get; private set; }
        #endregion

        /// <summary>
        /// Initializes the <see cref="TranslationManager"/> class.
        /// </summary>
        static TranslationManager()
        {
            Instance = new TranslationManager();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="TranslationManager"/> class from being created.
        /// </summary>
        private TranslationManager()
        {

        }

        public event EventHandler LanguageChanged;
        private void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string Translate(string key)
        {
            var provider = TranslationProvider;
            string translatedValue = null;
            if (provider != null)
            {
                translatedValue = provider.TranslateString(key);
            }
            return translatedValue ?? string.Format("!{0}!", key);

        }


        /// <summary>
        /// Translates the specified enums.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enums">The enums.</param>
        /// <returns></returns>
        public IEnumerable<NameValuePair> Translate<T>(IEnumerable<T> enums)
        {
            var typeName = typeof(T).Name;
            return from e in enums
                   let name = Translate($"{typeName}_{e}")
                   select new NameValuePair(name, e);
        }

        public IEnumerable<NameValuePair> Translate<T>()
        {
            return Translate(EnumHelper.GetValues<T>());
        }
    }
}
