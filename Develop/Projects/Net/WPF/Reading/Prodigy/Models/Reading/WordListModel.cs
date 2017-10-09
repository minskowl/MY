using System.IO;
using System.Windows.Input;
using Prodigy.Models.Core;
using Reading.Core;
using Savchin.Wpf.Input;

namespace Prodigy.Models.Reading
{
    public class WordListModel : BaseModel
    {
        #region Properties


        public override string Title => "Список слов";

        public ICommand SaveCommand { get; private set; }



        private string _text;
        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        /// <value>The name.</value> 
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value) return;
                _text = value;
                OnPropertyChanged("Text");
            }
        }


        #endregion

        public WordListModel()
        {
            SaveCommand = new DelegateCommand(OnSaveCommandExecute);
            Text = File.ReadAllText(ResourceProvider.WordFile);
        }

        private void OnSaveCommandExecute()
        {
            File.WriteAllText(ResourceProvider.WordFile, Text.Trim());
        }
    }
}
