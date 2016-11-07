using System.ComponentModel;

namespace Savchin.ComponentModel
{
    public class BaseObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(params string[] args)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                foreach (var s in args)
                {
                         handler(this, new PropertyChangedEventArgs(s));
                }
           
        }
    }
}
