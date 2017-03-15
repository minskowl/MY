using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Savchin.Core;
using Savchin.Wpf.Controls;
using Savchin.Wpf.Controls.Localization;
using Savchin.Wpf.Input;

namespace Prodigy.Models.Core
{
    public enum PlaybackMode
    {
        Forward,
        Backward,
        Random,
    }
    public abstract class ListModel<T> : SpeakModel
    {

        #region Properties
        private int _itemsCount;
        /// <summary>
        /// Gets or sets the ItemsCount.
        /// </summary>
        /// <value>The name.</value> 
        public int ItemsCount
        {
            get { return _itemsCount; }
            set { Set(ref _itemsCount, value); }
        }


        private T _selectedItem;
        /// <summary>
        /// Gets or sets the SelectedItem.
        /// </summary>
        /// <value>The name.</value> 
        public T SelectedItem
        {
            get { return _selectedItem; }
            set { Set(ref _selectedItem, value); }
        }


        private bool _itemsRepeatable;
        /// <summary>
        /// Gets or sets the ItemsRepetable.
        /// </summary>
        /// <value>The name.</value> 
        public bool ItemsRepetable
        {
            get { return _itemsRepeatable; }
            set { Set(ref _itemsRepeatable, value); }
        }

        private PlaybackMode _playbackMode;
        /// <summary>
        /// Gets or sets the PlaybackMode.
        /// </summary>
        /// <value>The name.</value> 
        public PlaybackMode PlaybackMode
        {
            get { return _playbackMode; }
            set
            {
                if (_playbackMode == value) return;
                _playbackMode = value;
                OnPlaybackModeChanged();
            }
        }



        private List<T> _itemList;
        private List<T> ItemList
        {
            get
            {
                if (_itemList == null)
                {
                    _itemList = BuildList();
                    ItemsCount = _itemList.Count;
                }
                return _itemList;
            }
        }


        /// <summary>
        /// Gets the playback modes.
        /// </summary>
        public NameValuePair[] PlaybackModes { get; private set; }


        /// <summary>
        /// Gets the next item command.
        /// </summary>
        public ICommand NextItemCommand { get; private set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ListModel&lt;T&gt;"/> class.
        /// </summary>
        protected ListModel()
        {
            NextItemCommand = new RelayCommand(OnNextItemCommandExecute);
            PlaybackModes = TranslationManager.Instance.Translate<PlaybackMode>().ToArray();
        }


        #region Protected

        protected virtual string GetSpeakText()
        {
            return SelectedItem.ToString();
        }

        protected abstract List<T> BuildList();

        /// <summary>
        /// Called when [setting changed].
        /// </summary>
        /// <param name="name">The name.</param>
        protected override void OnSettingChanged(string name)
        {
            base.OnSettingChanged(name);

            ResetItemList();
        }

        /// <summary>
        /// Resets the item list.
        /// </summary>
        protected void ResetItemList()
        {
            _itemList = null;
        }


        protected void SetNewItem()
        {
            start:

            if (ItemList.Count == 0)
            {
                if (WpfMessageBox.Show(string.Empty, "Список пуст. Заполнить список заново?", MessageButton.YesNo, MessageImage.Question, MessageResult.No) == MessageResult.No)
                    return;
                ResetItemList();
                goto start;
            }


            SelectedItem = GetNewItem();
            ItemsCount = ItemList.Count;
            if (Equals(SelectedItem, default(T)))
                WpfMessageBox.Show(string.Empty, "Список пуст.");
        }
        #endregion

        private void OnNextItemCommandExecute()
        {
            using (OverrideCursor.CreateWait())
                Speak(GetSpeakText());

            SetNewItem();

        }
        private T GetNewItem()
        {
            var newValue = GetNextItem();
            while (Equals(newValue, SelectedItem))
            {
                newValue = GetNextItem();
            }
            return newValue;
        }

        private int _index;
        private void OnPlaybackModeChanged()
        {
            _index = (PlaybackMode == PlaybackMode.Backward) ? ItemList.Count - 1 : 0;
            OnSettingChanged("PlaybackMode");
        }

        private T GetNextItem()
        {
            switch (PlaybackMode)
            {
                case PlaybackMode.Forward:
                    if (_index == ItemList.Count)
                    {
                        if (ItemsRepetable)
                        {
                            _index = 0;
                        }
                        else
                        {
                            return default(T);
                        }
                    }

                    return ItemList[_index++];
                case PlaybackMode.Backward:
                    if (_index == 0)
                    {
                        if (ItemsRepetable)
                        {
                            _index = ItemList.Count - 1;
                        }
                        else
                        {
                            return default(T);
                        }
                    }

                    return ItemList[_index--];
                case PlaybackMode.Random:
                    return ItemsRepetable ? Randomizer.GetFromArray<T>(ItemList) :
                Randomizer.ExctractFromArray(ItemList);

            }
            return default(T);
        }
    }
}
