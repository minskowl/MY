using System.Linq;
using Reading.Core;
using Reading.Properties;
using Savchin.Core;
using Savchin.Wpf.Controls.Localization;

namespace Reading.Models
{
    public class SyllablesModel : SyllablesModelBase
    {

        #region Properties
        private readonly Primer _primer = new Primer();
        /// <summary>
        /// Gets the title.
        /// </summary>
        public override string Title => "Слоги";


        private SelectionMode _mode;
        /// <summary>
        /// Gets or sets the Operation.
        /// </summary>
        /// <value>The name.</value> 
        public SelectionMode Mode
        {
            get { return _mode; }
            set
            {
                if (_mode == value) return;
                _mode = value;

                OnSettingChanged("Operation");
            }
        }


        private SyllablesTypes _type;
        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The name.</value> 
        public SyllablesTypes Type
        {
            get { return _type; }
            set
            {
                if (_type == value) return;
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        public NameValuePair[] Modes { get; set; }
        public NameValuePair[] Types { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SyllablesModel"/> class.
        /// </summary>
        public SyllablesModel()
        {
          
            Modes = TranslationManager.Instance.Translate<SelectionMode>().ToArray();
            Types = TranslationManager.Instance.Translate<SyllablesTypes>().ToArray();
        }

        /// <summary>
        /// Initializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected override void Initialize(Settings settings)
        {
            base.Initialize(settings);
            Mode = (SelectionMode)settings.SyllablesMode;
            Type = (SyllablesTypes) settings.SyllablesTypes;
            SetSyllable();
        }
        /// <summary>
        /// Saves the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected override void SaveSettings(Settings settings)
        {
            base.SaveSettings(settings);

            settings.SyllablesMode = (int)Mode;
            settings.SyllablesTypes = (int)Type;
        }

        protected override void SetSyllable()
        {
            SelectedItem = _primer.GetSyllable(Mode,Type);
        }
    }
}
