using Prodigy.Properties;
using Reading.Core;
using Savchin.Wpf.Controls.Localization;

namespace Prodigy.Models.Core
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

        protected TranslationManager Translation
        {
            get { return TranslationManager.Instance; }
        }
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
        protected virtual void OnSettingChanged(string name)
        {
            if (_ingoreChanges) return;

            SaveSettings(Settings.Default);
            Settings.Default.Save();
            OnPropertyChanged(name);
        }

        protected virtual void SaveSettings(Settings settings)
        {

        }
    }
}
