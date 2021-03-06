﻿using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Reading.Core;
using Savchin.Core;
using Savchin.Wpf.Controls.Localization;

namespace Reading.Models
{
    public sealed class LetterModel : SyllablesModelBase
    {
        public string TipImage
        {
            get { return _tipImage; }
            set { Set(ref _tipImage, value); }
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
                OnSettingChanging(ref _mode, value);
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
                Set(ref _type, value);
            }
        }

        public NameValuePair[] Modes { get; set; }
        public NameValuePair[] Types { get; set; }

        private readonly Primer _primer = new Primer();
        private string _tipImage;
       

        public override string Title
        {
            get { return "Буквы"; }
        }

        public LetterModel()
        {

            TipCommand = new RelayCommand(OnTipCommand);
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
