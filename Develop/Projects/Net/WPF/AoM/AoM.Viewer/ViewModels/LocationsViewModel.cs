using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AoM.Viewer.Data;
using Savchin.Collection.Generic;
using Savchin.Wpf.Core;
using Savchin.Wpf.Input;

namespace AoM.Viewer.ViewModels
{
    public class LocationsViewModel : ViewModelBase
    {
        public ObservableCollection<Location> Locations { get; set; }



        private Location _selectedLocation;

        public Location SelectedLocation
        {
            get => _selectedLocation;
            set => Set(ref _selectedLocation, value, nameof(SelectedLocation));
        }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public LocationsViewModel()
        {
            Locations = new ObservableCollection<Location>(SourceData.Locations);
            AddCommand=new DelegateCommand(OnAddCommand);
            SaveCommand=new DelegateCommand(OnSaveCommand);
        }

        private void OnSaveCommand()
        {
            if(Locations.IsEmpty())
                return;

            SourceData.Locations.Clear();
            SourceData.Locations.AddRange( Locations.ToList());
            SourceData.SaveLocations();
        }

        private void OnAddCommand()
        {
            var location= new Location();
            Locations.Add(location);
            SelectedLocation = location;

        }
    }
}
