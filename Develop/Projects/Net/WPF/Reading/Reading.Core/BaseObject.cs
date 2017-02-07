using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Reading.Core
{
    public class BaseObject : INotifyPropertyChanged 
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected  void OnProperty([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
