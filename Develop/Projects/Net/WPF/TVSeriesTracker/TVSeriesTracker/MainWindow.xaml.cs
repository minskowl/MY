using System.Windows;
using System.Windows.Controls.Primitives;
using Savchin.Wpf.Controls.Localization;
using TVSeriesTracker.Core;
using TVSeriesTracker.Models;
using TVSeriesTracker.Views;

namespace TVSeriesTracker
{
    public partial class MainWindow
    {
        private InfoBalloon _infoBalloon;
        private InfoBalloon Balloon {
            get
            {
                if (_infoBalloon == null)
                {
                    _infoBalloon= new InfoBalloon();
                    _infoBalloon.MouseDoubleClick += _infoBalloon_MouseDoubleClick;
                }
                return _infoBalloon;
            }
        }

        
        readonly MainWindowModel _model = new MainWindowModel();
        public MainWindow()
        {
            Visibility=Visibility.Hidden;

            InitializeComponent();
            DataContext = _model;
            _model.CanSee += _model_CanSee;
  
        }

        void _infoBalloon_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _model.DisplayPage = Pages.WatchList;
            _model.HiddenMode = false;
            Balloon.CloseBaloon();
        }

        void _model_CanSee(object sender, MoviesEventArgs e)
        {
            if (NotifyIcon.IsPopupOpen)return;

            Balloon.Title = TranslationManager.Instance.Translate("HasNewSeries");
            var builder = new System.Text.StringBuilder();

            foreach (var movie in e.Movies)
            {
                builder.AppendLine(movie.Title);
            }
            Balloon.Text = builder.ToString();
            

            
            //show balloon and close it after 4 seconds
            NotifyIcon.ShowCustomBalloon(Balloon, PopupAnimation.Slide, 4000);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = _model.OnClosing();

            base.OnClosing(e);
        }

 

   
    }
}
