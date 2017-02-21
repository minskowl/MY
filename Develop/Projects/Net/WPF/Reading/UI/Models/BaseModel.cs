using System.Runtime.CompilerServices;
using Reading.Core;
using Reading.Properties;
using Savchin.Wpf.Controls.Localization;
using Savchin.Wpf.Core;

namespace Reading.Models
{
    public abstract class BaseModel : ObjectBase
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
            set { Set(ref _status, value); }
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
        protected void OnSettingChanging<T>(ref T field, T newValue, [CallerMemberName] string name = "")
        {
            if (_ingoreChanges) return;
            Set(ref field, newValue, name);
            SaveSettings(Settings.Default);
            Settings.Default.Save();
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
