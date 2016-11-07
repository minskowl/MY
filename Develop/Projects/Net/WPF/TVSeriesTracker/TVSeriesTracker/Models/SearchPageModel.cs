using System;
using System.Linq;
using System.Windows;
using Savchin.Collection.Generic;
using Savchin.Wpf.Input;
using TVSeriesTracker.Core;
using TVSeriesTracker.Imdb;
using TVSeriesTracker.Properties;

namespace TVSeriesTracker.Models
{
    class SearchPageModel : ModelBase
    {
        #region Properties
        private ImdbManager _manager = new ImdbManager();
        private DataBase _dataBase=new DataBase();
        private ImdbResult _result;

        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand<MovieModel> AddToWatchCommand { get; private set; }
        public ImdbResult Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        public ObservableCollectionEx<MovieModel> Items { get; private set; }
        public int[] Limits { get; private set; }
        public Array Types { get; private set; }

        public ImdbRequest Request { get; private set; }
        #endregion

        public SearchPageModel()
        {
            SearchCommand = new DelegateCommand(OnSearch);
            AddToWatchCommand = new DelegateCommand<MovieModel>(OnAdd);

            Items = new ObservableCollectionEx<MovieModel>();

            Limits = Enumerable.Range(1, 10).ToArray();
            Types = Enum.GetValues(typeof(MovieType));

            Request = new ImdbRequest
                {
                    Text = "House",
                    Limit = Limits.Last()
                };
        }

        private void OnAdd(MovieModel obj)
        {
            if(obj==null)return;

            _dataBase.AddMovie(new Movie
                {
                    ImdbId = obj.Imdb_id,
                    Title = obj.Title
                });
        }

        private void OnSearch()
        {
            Result = _manager.Search(Request);
            if (Result.IsSuccess)
            {
                 Items.Fill(Result.Items.Select(e => new MovieModel(e, null)));
            }
            else
            {
                Items.Clear();
                MessageBox.Show(Result.ReasonPhrase, "Search result :: " + Result.StatusCode);
        
            }
        }
    }
}
