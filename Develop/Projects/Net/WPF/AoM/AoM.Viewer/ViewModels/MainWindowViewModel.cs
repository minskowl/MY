using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AoM.Viewer.Data;
using Newtonsoft.Json;
using Savchin.Wpf.Core;
using Savchin.Wpf.Input;

namespace AoM.Viewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Data

        Dictionary<string, List<Craft>> _crafts;
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
            get { return _items; }
            set { Set(ref _items, value); }
        }




        private Craft _selectedItem;

        public Craft SelectedItem
        {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value); }
        }
        private string _result;


        public string Result
        {
            get => _result;
            set { Set(ref _result, value); }
        }
        public ICommand ComputeCommand { get; }

        #endregion

        public MainWindowViewModel()
        {
            Heroes = JsonConvert.DeserializeObject<Hero[]>(File.ReadAllText(@"Data\\heroes.json")).OrderBy(e => e.Name).ToArray();
            _crafts = JsonConvert.DeserializeObject<Dictionary<string, List<Craft>>>(File.ReadAllText(@"Data\\craft.json"));
            Levels = Enumerable.Range(1, 10).ToArray();
            ComputeCommand = new DelegateCommand(OnComputeCommand);
            _levelFrom = 1;
            _levelTo = 2;
        }

        private void OnComputeCommand()
        {
            if (SelectedHero == null)
            {
                MessageBox.Show("Select hero");
                return;
            }


            Items = SelectedHero.Gears
                .Where(e => e.Level >= LevelFrom && e.Level <= LevelTo)
                .GroupBy(e => e.Name)
                .OrderBy(e => e.Key)
                .Select(e => new Craft
                {
                    Count = e.Count(),
                    Name = e.Key
                }).ToArray();


            OnPropertyChanged(nameof(Items));

        }
    }
}
