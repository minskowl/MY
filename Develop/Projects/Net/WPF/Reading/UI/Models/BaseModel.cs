using System.Runtime.CompilerServices;
using Reading.Core;
using Reading.Properties;
using Savchin.Logging;
using Savchin.Wpf.Controls.Localization;

namespace Reading.Models
{
    public abstract class BaseModel : BaseObject
    {
        private readonly bool _ingoreChanges;
 
        /// <summary>
        /// Gets the title.
        /// </summary>
        public abstract string Title { get; }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status == value) return;
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        protected TranslationManager Translation => TranslationManager.Instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModel"/> class.
        /// </summary>
        protected BaseModel()
        {
            _ingoreChanges = true;
            Initialize(Settings.Default);
            _ingoreChanges = false;
        }


        /// <summary>
        /// Initializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected virtual void Initialize(Settings settings)
        {
            
        }

        /// <summary>
        /// Called when [setting changed].
        /// </summary>
        /// <param name="name">The name.</param>
        protected  void OnSettingChanging([CallerMemberName] string name="")
        {
            if (_ingoreChanges) return;

            SaveSettings(Settings.Default);
            Settings.Default.Save();
            OnPropertyChanged(name);
            OnSettingChanged();
        }

        protected virtual void OnSettingChanged()
        {

        }

        protected virtual void SaveSettings(Settings settings)
        {

        }
    }
}
