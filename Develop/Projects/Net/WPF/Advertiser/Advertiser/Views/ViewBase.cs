using System.ComponentModel;
using Advertiser.Core;
using Savchin.Logging;

namespace Advertiser.Views
{
    public abstract class ViewBase : INotifyPropertyChanged
    {
        public string Title { get; protected set; }
        protected ILogger Log { get { return AdvContext.Current.Log; } }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        public override string ToString()
        {
            return Title;

        }
    }
}
