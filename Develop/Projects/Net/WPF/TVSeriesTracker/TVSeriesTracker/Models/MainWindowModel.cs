using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Savchin.Wpf.Input;
using TVSeriesTracker.Core;
using TVSeriesTracker.Properties;

namespace TVSeriesTracker.Models
{
    class MainWindowModel : ModelBase
    {
        #region Properties
        private static readonly DataBase DataBase = new DataBase();
        private bool _hiddenMode;
        private DispatcherTimer _dispatcherTimer;

        public string DisplayPage
        {
            get { return _displayPage; }
            set
            {
                _displayPage = value;
                OnPropertyChanged("DisplayPage");
            }
        }

        public Visibility Visibility
        {
            get { return HiddenMode ? Visibility.Hidden : Visibility.Visible; }
            set { HiddenMode = value != Visibility.Visible; }
        }

        public ICommand ShowCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand CheckNewSeriesCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }

        public bool HiddenMode
        {
            get { return _hiddenMode; }
            set
            {
                _hiddenMode = value;
                OnPropertyChanged("Visibility");
                _dispatcherTimer.IsEnabled = _hiddenMode;
            }
        }
        Lazy<IWatchListPageModel> WatchList = new Lazy<IWatchListPageModel>(
        () => App.Container.GetInstance<IWatchListPageModel>()
    );

        private string _displayPage;

        public event EventHandler<MoviesEventArgs> CanSee;



        #endregion

        public MainWindowModel()
        {
            ShowCommand = new DelegateCommand(OnShowCommand);
            ExitCommand = new DelegateCommand(OnExitCommand);
            CheckNewSeriesCommand = new DelegateCommand(OnCheckNewSeriesCommand);
            SettingsCommand = new DelegateCommand(OnSettingsCommand);

            DisplayPage = DataBase.GetMovies().Length == 0 ? Pages.Search : Pages.WatchList;

            _dispatcherTimer = new DispatcherTimer(new TimeSpan(Settings.Default.CheckInterval, 0, 0), DispatcherPriority.Normal, OnTimer, Dispatcher.CurrentDispatcher);

            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && args[1] == "autorun" && Settings.Default.CloseToTray)
            {
                HiddenMode = true;
                WatchList.Value.Items.CollectionChanged += Items_CollectionChanged;
                ChekNewSeries();
             }
        }

    

        void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (!HiddenMode) return;

            var whatCanSee = WatchList.Value.Items.Where(i => i.HasNewEpisodes).ToArray();
            if (whatCanSee.Length > 0)
                OnCanSee(new MoviesEventArgs(whatCanSee));
        }

        public bool OnClosing()
        {
            if (Settings.Default.CloseToTray)
            {
                HiddenMode = true;
                return true;
            }


            return false;

        }


        private void ChekNewSeries()
        {
            WatchList.Value.FillList();

        }

        private void OnTimer(object sender, EventArgs e)
        {
            ChekNewSeries();
        }

        protected virtual void OnCanSee(MoviesEventArgs e)
        {
            EventHandler<MoviesEventArgs> handler = CanSee;
            if (handler != null) handler(this, e);
        }

        #region Commands
        private void OnSettingsCommand()
        {
            HiddenMode = false;
            DisplayPage = Pages.Settings;
        }

        private void OnCheckNewSeriesCommand()
        {
            ChekNewSeries();
        }
        private void OnExitCommand()
        {
            Application.Current.Shutdown();
        }

        private void OnShowCommand()
        {
            HiddenMode = false;
        }
        #endregion


    }
}
