using System.ComponentModel;

namespace Advertiser.Entities
{
    public abstract class ObjectBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="property">The property.</param>
        protected virtual void OnPropertyChanged(string property)
        {
            var tmp = PropertyChanged;
            if(tmp!=null)
                tmp(this,new PropertyChangedEventArgs(property));
        }
    }

    public class Entity : ObjectBase, IEntity
    {
        
        private int _id;
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        
        
        public override event PropertyChangedEventHandler PropertyChanged;
    }
}