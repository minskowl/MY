using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reading.Core;
using Savchin.Core;
using Savchin.Wpf.Controls.Localization;

namespace Reading.Models
{
   public sealed class LetterModel : SyllablesModelBase
    {
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


        private LettersTypes _type= LettersTypes.All;
        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The name.</value> 
        public LettersTypes Type
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

        private readonly Primer _primer = new Primer();

        public override string Title {
            get { return "Буквы"; }
        }

        public LetterModel()
        {
            Modes = TranslationManager.Instance.Translate<SelectionMode>().ToArray();
            Types = TranslationManager.Instance.Translate<LettersTypes>().ToArray();
            SetSyllable();
        }

        protected override void SetSyllable()
        {
            SelectedItem =new string(_primer.GetLetter( Type,Mode),1).ToUpper(); 
        }
    }
}
