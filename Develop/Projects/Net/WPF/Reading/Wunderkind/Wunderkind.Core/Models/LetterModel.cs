using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CITrader.Controls.Commands;
using Reading.Core;
using Savchin.Core;
using Savchin.Wpf.Controls.Localization;
using Wunderkind.Core;

namespace Reading.Models
{
    public sealed class LetterModel : SyllablesModelBase
    {
        public string TipImage
        {
            get { return _tipImage; }
            set
            {
                _tipImage = value;
                OnPropertyChanged(nameof(TipImage));
            }
        }

        public ICommand TipCommand { get; private set; }

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


        private LettersTypes _type = LettersTypes.All;
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
        private string _tipImage;
        private bool _isTipVisible;

        public override string Title
        {
            get { return "Буквы"; }
        }

        public LetterModel()
        {

            TipCommand = new DelegateCommand(OnTipCommand);
            Modes = TranslationManager.Instance.Translate<SelectionMode>().ToArray();
            Types = TranslationManager.Instance.Translate<LettersTypes>().ToArray();
            SetSyllable();
        }

        private void OnTipCommand()
        {
            TipImage = $"../Resources/Letters/{SelectedItem}/1.jpg";
       
        }

        protected override void SetSyllable()
        {
            SelectedItem = new string(_primer.GetLetter(Type, Mode), 1).ToUpper();
            TipImage = null;
        }
    }
}
