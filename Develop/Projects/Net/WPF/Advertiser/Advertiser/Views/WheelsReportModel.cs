using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Input;
using Advertiser.Entities;
using Savchin.Core;
using Savchin.Wpf.Input;

namespace Advertiser.Views
{
    internal class WheelsReportModel
    {

        public ICommand SortCommand { get; private set; }
        public ICollectionView Items
        {
            get;
            set;
        }

        public WheelsReportModel(DataBase data)
        {
            SortCommand = new DelegateCommand<string>(OnSort);
           var wheels= data.Wheels.OrderBy(e => e.Size).Select(e => new DataRow(e)).ToArray();

            Items = CollectionViewSource.GetDefaultView(wheels);
        }

        private void OnSort(string obj)
        {
            Items.SortDescriptions.Clear();
            Items.SortDescriptions.Add(new SortDescription(obj, ListSortDirection.Ascending));


        }

        public class DataRow
        {
            private readonly Wheels _e;
            public string Title { get; private set; }
            public List<string> Images
            {
                get { return _e.Images; }
            }
            public int Id
            {
                get { return _e.Id; }
            }
            public WheelSize Size
            {
                get { return _e.Size; }
            }
            public string Manufacturer
            {
                get { return _e.Manufacturer; }
            }
            public DataRow(Wheels e)
            {
                _e = e;
                Title = string.Format("#{5} {0} {1} {3}רע {2} {4} ", e.Size, e.Manufacturer,
                                      e.PriceText, e.Count, e.Season.GetDescription(), e.Id);
            }
        }
    }
}