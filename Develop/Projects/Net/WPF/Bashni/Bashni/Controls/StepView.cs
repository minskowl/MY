using System.ComponentModel;
using Bashni.Game;

namespace Bashni.Controls
{
    public class StepView : INotifyPropertyChanged
    {
        private readonly Step _data;
        private bool isExpanded;

        public StepView(Step step)
        {
            _data = step;
        }

        public Step Key
        {
            get { return _data; }
        }

        public int Level
        {
            get { return _data.Number; }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        public string ShortName
        {
            get { return _data.ToString(); }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}