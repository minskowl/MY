using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Reading.Core;
using Reading.Speach;
using Savchin.Wpf.Input;

namespace Reading.Models
{
    public abstract class SyllablesModelBase : SpeakModel
    {
        
        private string _selectedItem;


        /// <summary>
        /// Gets or sets the selected syllable.
        /// </summary>
        /// <value>
        /// The selected syllable.
        /// </value>
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value) return;
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        /// <summary>
        /// Gets the read command.
        /// </summary>
        public ICommand NextItemCommand { get; private set; }

        protected void OnRelayCommandExecute()
        {
            using (OverrideCursor.CreateWait())
            {
                Speak(SelectedItem);
                SetSyllable();
            }
        }

        protected abstract void SetSyllable();

        protected SyllablesModelBase()
        {
            NextItemCommand = new RelayCommand(OnRelayCommandExecute);
        }
    }
}