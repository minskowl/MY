using System.Windows.Input;
using Savchin.ComponentModel;
using TVSeriesTracker.Imdb;

namespace TVSeriesTracker.Models
{
    public class EpisodeModel : BaseObject
    {
        #region Properties
        private readonly EpisodeInfo _info;
        private bool _isLastWatched;

        public ICommand MarkWatchedCommand { get; set; }

        public bool IsLastWatched
        {
            get { return _isLastWatched; }
            set
            {
                if (_isLastWatched == value)
                    return;
                _isLastWatched = value;
                OnPropertyChanged("IsLastWatched");
            }
        }

        public string Season
        {
            get { return _info.Season; }
        }
        public string Episode
        {
            get { return _info.Episode; }
        }
        #endregion

        public EpisodeModel(EpisodeInfo info)
        {
            _info = info;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", _info.Episode, _info.Title, _info.Date);
        }
    }
}
