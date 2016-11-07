using System.ComponentModel;

namespace EffectiveSoft.SilverlightDemo.Core
{
    /// <summary>
    /// Class representing an untyped pair of values.
    /// </summary>
    public class Pair : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the first value.
        /// </summary>
        public object First
        {
            get
            {
                return _first;
            }
            set
            {
                _first = value;
                OnPropertyChanged("First");
            }
        }

        /// <summary>
        /// Stores the value of the First property.
        /// </summary>
        private object _first;

        /// <summary>
        /// Gets or sets the second value.
        /// </summary>
        public object Second
        {
            get
            {
                return _second;
            }
            set
            {
                _second = value;
                OnPropertyChanged("Second");
            }
        }

        /// <summary>
        /// Stores the value of the Second property.
        /// </summary>
        private object _second;


        /// <summary>
        /// Initializes a new instance of the <see cref="Pair"/> class.
        /// </summary>
        public Pair()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Pair"/> class.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        public Pair(object first,object second)
        {
            _first = first;
            _second = second;
        }

        /// <summary>
        /// Implements the INotifyPropertyChanged interface.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property that changed.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
