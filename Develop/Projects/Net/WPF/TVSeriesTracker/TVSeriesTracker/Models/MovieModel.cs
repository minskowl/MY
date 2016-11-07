using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Data;
using Savchin.Collection.Generic;
using Savchin.ComponentModel;
using Savchin.Wpf.Input;
using TVSeriesTracker.Core;
using TVSeriesTracker.Imdb;
using Savchin.Text;
using TVSeriesTracker.Properties;

namespace TVSeriesTracker.Models
{
    public class MovieModel : BaseObject
    {
        private DelegateCommand<EpisodeModel> MarkWatchedCommand;
        private EpisodeModel[] _episodes;
        private static IComparer<string> DefaultComparer = new AlphanumComparator();
        private MovieInfo _info;
        private bool _hasNewEpisodes;

        #region Properties

        public bool HasNewEpisodes
        {
            get { return _hasNewEpisodes; }
            private set
            {

                _hasNewEpisodes = value;
                OnPropertyChanged("HasNewEpisodes");
            }
        }

        public String Genres
        {
            get { return _info.Genres.Join(", "); }
        }
        public ICollectionView EpisodesView
        {
            get;
            private set;
        }


  

        public string Imdb_id
        {
            get { return _info.Imdb_id; }
        }
        public string Imdb_url
        {
            get { return _info.Imdb_url; }
        }
        public float Rating { get { return _info.Rating; } }
        public string Title { get { return _info.Title; } }
        public string Type { get { return _info.Type; } }
        public string Plot { get { return _info.Plot; } }
        public string Plot_simple { get { return _info.Plot_simple; } }
        public string Poster { get { return _info.Poster; } }
        public int Year { get { return _info.Year; } }
        #endregion

        public MovieModel(MovieInfo info, EpisodeInfo lastWatched)
        {
            MarkWatchedCommand = new DelegateCommand<EpisodeModel>(OnMarkWatchedCommand);
            _info = info;

            FillEpisodesInfo();

            SetLastWatched(lastWatched == null ? null : _episodes.FirstOrDefault(e => e.Season == lastWatched.Season && e.Episode == lastWatched.Episode));
        }


        protected void SetLastWatched(EpisodeModel lastWatched)
        {
            _episodes.Foreach(e => e.IsLastWatched = e == lastWatched);
            var unWatchedCount = lastWatched == null ? _episodes.Length : _episodes.Length - _episodes.IndexOf(lastWatched) - 1;
            HasNewEpisodes = unWatchedCount >= Settings.Default.EpisdeNotifierDelta;
        }


        private void FillEpisodesInfo()
        {
            if (_info.Episodes == null || _info.Episodes.Count == 0)
            {
                _episodes=new EpisodeModel[0];
                return;
            }


            _episodes = _info.Episodes
                .OrderBy(e => e.Season, DefaultComparer)
                .ThenBy(e => e.Episode, DefaultComparer)
                .Select(e => new EpisodeModel(e)
                    {
                        MarkWatchedCommand = MarkWatchedCommand
                    })
                .ToArray();

            var viewSource = new CollectionViewSource { Source = _episodes };
            viewSource.GroupDescriptions.Add(new PropertyGroupDescription("Season"));
            EpisodesView = viewSource.View;


        }

        protected virtual void OnMarkWatchedCommand(EpisodeModel obj)
        {

        }

    }
}
