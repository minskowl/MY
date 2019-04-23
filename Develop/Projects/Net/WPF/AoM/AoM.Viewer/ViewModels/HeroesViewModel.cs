using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AoM.Viewer.Data;
using Savchin.Wpf.Core;
using Savchin.Wpf.Input;

namespace AoM.Viewer.ViewModels
{
    public class HeroesViewModel : ViewModelBase
    {
        #region Data

        private readonly Dictionary<string, List<Craft>> _crafts;
        public int[] Levels { get; }
        public Hero[] Heroes { get; }




        private int _levelFrom;

        public int LevelFrom
        {
            get => _levelFrom;
            set => Set(ref _levelFrom, value);
        }



        private int _levelTo;


        public int LevelTo
        {
            get => _levelTo;
            set { Set(ref _levelTo, value); }
        }



        private Hero _selectedHero;

        public Hero SelectedHero
        {
            get { return _selectedHero; }
            set { Set(ref _selectedHero, value); }
        }



        private Craft[] _items;


        public Craft[] Items
        {
            get => _items;
            set { Set(ref _items, value, nameof(Items)); }
        }




        private Craft _selectedItem;

        public Craft SelectedItem
        {
            get => _selectedItem;
            set { Set(ref _selectedItem, value, OnSelectedItemChanged,nameof(SelectedItem)); }
        }



        private Craft[] _subItems;


        public Craft[] SubItems
        {
            get { return _subItems; }
            set { Set(ref _subItems, value, nameof(SubItems)); }
        }

        private string _result;


        public string Result
        {
            get => _result;
            set { Set(ref _result, value); }
        }
        public ICommand ComputeCommand { get; }

        #endregion

        public HeroesViewModel()
        {
            Heroes = SourceData.Heroes.OrderBy(e => e.Name).ToArray();
            _crafts = SourceData.Crafts;
            Levels = Enumerable.Range(1, 10).ToArray();
            ComputeCommand = new DelegateCommand(OnComputeCommand);
            _levelFrom = 3;
            _levelTo = 4;
            _selectedHero = Heroes.FirstOrDefault();
        }

        private void OnComputeCommand()
        {
            if (SelectedHero == null)
            {
                MessageBox.Show("Select hero");
                return;
            }

            var tmp = SelectedHero.Gears
                .Where(e => e.Level >= LevelFrom && e.Level <= LevelTo)
                .GroupBy(e => e.Name)
                .OrderBy(e => e.Key)
                .Select(e => new Craft
                {
                    Count = e.Count(),
                    Name = e.Key
                }).ToArray();
            Items = tmp;



        }
        private void OnSelectedItemChanged()
        {
            if (SelectedItem == null)
            {
                SubItems = null;
                return;
            }

            List<Craft> res;

            _crafts.TryGetValue(SelectedItem.Name, out res);

            SubItems = res.ToArray();
            OnPropertyChanged(nameof(SubItems));

        }
    }
}
