using System.Linq;
using System.Windows.Input;
using Reading.Core;
using Savchin.Wpf.Controls.Localization;
using Savchin.Wpf.Input;

namespace Prodigy.Models.Reading
{
    public sealed class LetterModel : LetterModelBase
    {
        public string TipImage
        {
            get { return _tipImage; }
            set { Set(ref _tipImage, value); }
        }

        public ICommand TipCommand { get; private set; }




        private string _tipImage;


        public override string Title => "Буквы";

        public LetterModel()
        {

            TipCommand = new DelegateCommand(OnTipCommand);
            Modes = Translation.Translate<SelectionMode>().ToArray();
            Types = Translation.Translate<LettersTypes>().ToArray();
            SetSyllable();
        }

        private void OnTipCommand()
        {
            TipImage = $"../Resources/Letters/{SelectedItem}.jpg";

        }

        protected override void SetSyllable()
        {
            SelectedItem = GetLetter();
            TipImage = null;
        }
    }
}
