using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Savchin.Collection.Generic;
using Savchin.Core;
using TVSeriesTracker.Core;
using TVSeriesTracker.Imdb;

namespace TVSeriesTracker.Models
{
    public enum WatchListPageMode
    {
        All,
        UnWatched,
    }

    public interface IWatchListPageModel
    {
        ObservableCollectionEx<MovieModel> Items { get; }
        void FillList();
    }

    class WatchListPageModel : ModelBase, IWatchListPageModel
    {
        #region Properties
        private static IDataBase _dataBase;
        private  readonly IImdbManager Manager;

        private List<WatchListMovieModel> _models = new List<WatchListMovieModel>();
        private WatchListPageMode _selectedMode = WatchListPageMode.All;

        public ObservableCollectionEx<MovieModel> Items { get; private set; }

        public WatchListPageMode[] Modes { get; private set; }
        public WatchListPageMode SelectedMode
        {
            get { return _selectedMode; }
            set
            {
                if (_selectedMode == value) return;
                _selectedMode = value;
                OnModeChanged();
                OnPropertyChanged("SelectedMode");
            }
        }


        #endregion


        public WatchListPageModel(IImdbManager manager, IDataBase dataBase)
        {
            _dataBase = dataBase;
            Manager = manager;

            Items = new ObservableCollectionEx<MovieModel>();
            Modes = EnumHelper.GetValuesArray<WatchListPageMode>();

           
        }
        public override void OnLoaded()
        {
            base.OnLoaded();
            FillList();
        }
        private void OnModeChanged()
        {
            if (_models == null) return;
            Items.Fill(SelectedMode == WatchListPageMode.UnWatched ? _models.Where(e => e.HasNewEpisodes) : _models);
        }

        public async void FillList()
        {
            var movies = _dataBase.GetMovies();

            _models.Clear();

            var listIds = movies.Select(e => e.ImdbId).Split(10);

            foreach (var ids in listIds)
            {
                var res = await Manager.GetByIdsAsync(ids);

                if (res.IsSuccess)
                {
                    _models.AddRange(res.Items.Select(
                            e => new WatchListMovieModel(e, movies.FirstOrDefault(m => m.ImdbId == e.Imdb_id))).
                            ToArray());

                }
            }
            Items.Fill(_models);
        }

        public class WatchListMovieModel : MovieModel
        {

            private readonly Movie _movieRecord;
            public WatchListMovieModel(MovieInfo movie, Movie record)
                : base(movie, movie.Episodes.FirstOrDefault(e => e.Episode == record.Episode && e.Season == record.Season))
            {
                _movieRecord = record;

            }

            protected override void OnMarkWatchedCommand(EpisodeModel obj)
            {
                base.OnMarkWatchedCommand(obj);


                _movieRecord.Season = obj.Season;
                _movieRecord.Episode = obj.Episode;

                _dataBase.UpdateMovie(_movieRecord);


                SetLastWatched(obj);
            }

        }
    }


}
