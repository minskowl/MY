using System.Linq;
using System.Windows.Input;
using Reading.Core.Settings;
using Savchin.Core;
using Savchin.Wpf.Input;

namespace Reading.Models
{

    public class FindPairModel : SpeakModel
    {
        #region Properties

        private int _rows;
        public int Rows
        {
            get { return _rows; }
            set
            {
                if (_rows == value) return;
                _rows = value;
                OnSettingChanging();
            }
        }
        private int _columns;
        public int Columns
        {
            get { return _columns; }
            set
            {
                if (_columns == value) return;
                _columns = value;
                OnSettingChanging();
            }
        }

        private int _images;
        public int Images
        {
            get { return _images; }
            set
            {
                if (_images == value) return;
                _images = value;
                OnSettingChanging();
            }
        }

        public int[] ColumnsModes { get; private set; }
        public int[] RowsModes { get; private set; }
        public int[] ImagesModes { get; private set; }
        public int?[,] Cards { get; private set; }

        public override string Title => "Найти пару";

        public ICommand FillCommand { get; }

        #endregion

        public FindPairModel()
        {
            ColumnsModes = new[] { 2, 4, 6, 8, 10 };
            RowsModes = new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            ImagesModes = new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            FillCommand= new DelegateCommand(FillSamples);
        }




        protected override void Initialize(Properties.Settings settings)
        {
            base.Initialize(settings);

            if (settings.FindPairSettings == null)
                settings.FindPairSettings = new FindPairSettings();
            var s = settings.FindPairSettings;

            _rows = s.Rows;
            _columns = s.Columns;
            _images = s.Images;


            FillSamples();
        }



        protected override void SaveSettings(Properties.Settings settings)
        {
            base.SaveSettings(settings);
            var s = settings.FindPairSettings;

            s.Rows = _rows;
            s.Columns = _columns;
            s.Images = _images;

            FillSamples();
            OnPropertyChanged("ShowAnswers");
        }


        private void FillSamples()
        {
            Cards = new int?[Rows, Columns];
            var size = Rows * Columns;
            var variants = Enumerable.Range(0, size).ToList();
            while (variants.Count > 0)
            {
                var image = Randomizer.GetIntegerBetween(0, Images);
                var pos1 = Randomizer.GetFromArray<int>(variants);
                variants.Remove(pos1);
                var pos2 = Randomizer.GetFromArray<int>(variants);
                variants.Remove(pos2);

                SetCard(pos1, image);
                SetCard(pos2, image);
            }

            OnPropertyChanged("Cards");
        }

        private void SetCard(int pos, int card)
        {
            Cards[pos / Columns, pos % Columns] = card;
        }
    }
}
