using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Reading.Core
{
    public class BaseObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(ref T field, T newValue, Action onValueChanged = null,
            [CallerMemberName] string propertyName = null)
        {
            return SetProperty(ref field, newValue, onValueChanged, propertyName);
        }

        protected bool SetProperty<T>(ref T field, T newValue, Action onValueChanged, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            field = newValue;
            onValueChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
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
