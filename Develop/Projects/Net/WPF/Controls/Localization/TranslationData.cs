using System;
using System.Windows;
using System.ComponentModel;

namespace Savchin.Wpf.Controls.Localization
{
    public class TranslationData : IWeakEventListener, INotifyPropertyChanged, IDisposable 
    {
        #region Private Members

        private string _key;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationData"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public TranslationData( string key)
        {
            _key = key;
            LanguageChangedEventManager.AddListener(TranslationManager.Instance, this);
        }

        /// <summary> 
        /// Releases unmanaged resources and performs other cleanup operations before the 
        /// <see cref="TranslationData"/> is reclaimed by garbage collection. 
        /// </summary> 
        ~TranslationData()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Code to dispose the managed resources of the class 
                LanguageChangedEventManager.RemoveListener(TranslationManager.Instance, this);
            }
            // Code to dispose the un-managed resources of the class 
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
        
        public object Value
        {
            get
            {
                return TranslationManager.Instance.Translate(_key);
            }
        }

        #region IWeakEventListener Members

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(LanguageChangedEventManager))
            {
                OnLanguageChanged(sender, e);
                return true;
            }
            return false;
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            if( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs("Value"));
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
