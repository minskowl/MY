using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Advertiser.Views
{
    public interface IObservableCollection : IList, INotifyCollectionChanged, INotifyPropertyChanged
    {
    }
}
