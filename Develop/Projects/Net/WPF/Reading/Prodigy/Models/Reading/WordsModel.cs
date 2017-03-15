using System.Collections.Generic;
using System.Linq;
using Prodigy.Models.Core;
using Prodigy.Properties;
using Reading.Core;
using Savchin.Core;

namespace Prodigy.Models.Reading
{
    public class WordsModel : ListModel<Word>
    {
        #region Property


        private readonly WordsCollection _words = new WordsCollection();

        public override string Title => "Слова";

        #region Settings



        private int _wordSyllablesCountFrom;
        /// <summary>
        /// Gets or sets the WordSyllablesCountFrom.
        /// </summary>
        /// <value>The name.</value> 
        public int WordSyllablesCountFrom
        {
            get { return _wordSyllablesCountFrom; }
            set
            {
                if (_wordSyllablesCountFrom == value) return;
                _wordSyllablesCountFrom = value;
                OnSettingChanged("WordSyllablesCountFrom");
            }
        }


        private int _wordSyllablesCountTo;
        /// <summary>
        /// Gets or sets the WordSyllablesCountTo.
        /// </summary>
        /// <value>The name.</value> 
        public int WordSyllablesCountTo
        {
            get { return _wordSyllablesCountTo; }
            set
            {
                if (_wordSyllablesCountTo == value) return;
                _wordSyllablesCountTo = value;
                OnSettingChanged("WordSyllablesCountTo");
            }
        }


        private int _wordWidthFrom;
        /// <summary>
        /// Gets or sets the WordWidthFrom.
        /// </summary>
        /// <value>The name.</value> 
        public int WordWidthFrom
        {
            get { return _wordWidthFrom; }
            set
            {
                if (_wordWidthFrom == value) return;
                _wordWidthFrom = value;
                OnSettingChanged("WordWidthFrom");
            }
        }


        private int _wordWidthTo;
        /// <summary>
        /// Gets or sets the WordWidthTo.
        /// </summary>
        /// <value>The name.</value> 
        public int WordWidthTo
        {
            get { return _wordWidthTo; }
            set
            {
                if (_wordWidthTo == value) return;
                _wordWidthTo = value;
                OnSettingChanged("WordWidthTo");
            }
        }


        private bool _syllablesView;
        /// <summary>
        /// Gets or sets the SyllablesView.
        /// </summary>
        /// <value>The name.</value> 
        public bool SyllablesView
        {
            get { return _syllablesView; }
            set
            {
                if (_syllablesView == value) return;
                _syllablesView = value;
                OnSettingChanged("SyllablesView");
            }
        }

        public int[] WordLengths { get; private set; }
        public int[] SyllableCounts { get; private set; }
        #endregion



        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="WordsModel"/> class.
        /// </summary>
        public WordsModel()
        {
            SyllableCounts = _words.GetSyllableCounts();
            WordLengths = Enumerable.Range(3, 10).ToArray();
            PlaybackMode = PlaybackMode.Random;
        }

        /// <summary>
        /// Initializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected override void Initialize(Settings settings)
        {
            base.Initialize(settings);

            _wordWidthTo = settings.WordWidthTo;
            _wordWidthFrom = settings.WordWidthTo;
            _wordSyllablesCountFrom = settings.WordSyllablesCountFrom;
            _wordSyllablesCountTo = settings.WordSyllablesCountTo;
            _syllablesView = settings.WordSyllablesView;
            ItemsRepetable = settings.WordRepeatable;
        }

        protected override void SaveSettings(Settings settings)
        {
            base.SaveSettings(settings);

            settings.WordWidthTo = _wordWidthTo;
            settings.WordWidthTo = _wordWidthFrom;
            settings.WordSyllablesCountFrom = _wordSyllablesCountFrom;
            settings.WordSyllablesCountTo = _wordSyllablesCountTo;
            settings.WordRepeatable = ItemsRepetable;
            settings.WordSyllablesView = _syllablesView;
        }

        protected override string GetSpeakText()
        {
            return SelectedItem?.Text;
        }


        protected override List<Word> BuildList()
        {
            return _words.GetWords(new Range<int>(_wordSyllablesCountFrom, _wordSyllablesCountTo),new Range<int>(_wordWidthFrom, _wordWidthTo));
        }


    }
}
