using System.Collections.Generic;
using System.Linq;
using Reading.Core;
using Reading.Properties;
using Savchin.Core;

namespace Reading.Models
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
                OnSettingChanging(ref _wordSyllablesCountFrom, value);
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
                OnSettingChanging(ref _wordSyllablesCountTo, value);
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
                OnSettingChanging(ref _wordWidthFrom, value);
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
                OnSettingChanging(ref _wordWidthTo, value);
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
                OnSettingChanging(ref _syllablesView, value);
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

            SetNewItem();
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
