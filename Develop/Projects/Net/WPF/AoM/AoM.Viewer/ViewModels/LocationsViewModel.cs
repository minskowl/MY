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
        public ObservableCollection<LocationViewModel> Locations { get; set; }



        private LocationViewModel _selectedLocation;

        public LocationViewModel SelectedLocation
        {
            get => _selectedLocation;
            set => Set(ref _selectedLocation, value, nameof(SelectedLocation));
        }

        public ICommand AddCommand { get; }
        public ICommand SaveCommand { get; }
        public LocationsViewModel()
        {
            Locations = new ObservableCollection<LocationViewModel>(SourceData.Locations.Select(e=> new LocationViewModel(e)));
            AddCommand=new DelegateCommand(OnAddCommand);
            SaveCommand=new DelegateCommand(OnSaveCommand);
        }

        private void OnSaveCommand()
        {
            if(Locations.IsEmpty())
                return;

            SourceData.Locations.Clear();
            SourceData.Locations.AddRange( Locations.Select(e=>e.GetData()).ToArray());
            SourceData.SaveLocations();
        }

        private void OnAddCommand()
        {
            var location=new LocationViewModel( new Location());
            Locations.Add(location);
            SelectedLocation = location;

        }
    }
}
