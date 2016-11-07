using System.Collections.Generic;
using System.ComponentModel;
using Savchin.Core;

namespace Advertiser.Entities
{
    public enum WheelCondition
    {
        [Description()]
        None = 0,
        [Description("новые")]
        New = 1,
        [Description("б/у")]
        Used = 2,
        [Description("наварные")]
        Welded = 3,
    }

    public enum WheelSeason
    {
        [Description()]
        None = 0,
        [Description("летние")]
        Summer = 1,
        [Description("зимние")]
        Winter = 2,
        [Description("шипованые")]
        SnowTyre = 3,
        [Description("всесезонные")]
        AllSeasons = 4
    }

    public class Wheels : Advertisement
    {
        private string _manufacturer;
        public string Manufacturer
        {
            get { return _manufacturer; }
            set
            {
                if (_manufacturer == value || string.IsNullOrEmpty(value)) return;
                _manufacturer = value;
                OnPropertyChanged("Manufacturer");
            }
        }
        public string MainManufacturer
        {
            get { return _manufacturer.Split(new char[] {'/', '|'})[0]; }
        }
        private WheelSize _size;
        public WheelSize Size
        {
            get { return _size; }
            set
            {
                if (_size == value) return;

                if (_size != null) _size.PropertyChanged -= OnSizePropertyChanged;
                _size = value;
                if (_size != null) _size.PropertyChanged += OnSizePropertyChanged;
                OnPropertyChanged("Size");
                OnPropertyChanged("Title");
            }
        }

        public WheelSeason Season { get; set; }
        public WheelCondition Condition { get; set; }
        public int? Price { get; set; }
        public int Count { get; set; }


        public string Description { get; set; }
        public bool Auction { get; set; }

        public List<string> Images { get; set; }

        public string PriceText
        {
            get { return Price.HasValue ? Price + "$" : string.Empty; }
        }

        public string Title
        {
            get { return string.Format("#{0} {1} {2}", Id, MainManufacturer, Size); }
        }


        public string Subject
        {
            get { return string.Format("{0} {1} {2}шт", MainManufacturer, Size, Count); }
        }

        public string SeasonSubject
        {
            get { return string.Format("{0} {1}", Season.GetDescription(), Subject); }
        }

        public Wheels()
        {
            Images = new List<string>();
            Size = new WheelSize();
        }
        public override string ToString()
        {
            return string.Format("#{0} {1} {2}", Id, Manufacturer, Size);
        }

        private void OnSizePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Title");
        }
    }
}
